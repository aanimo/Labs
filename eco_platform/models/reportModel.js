const db = require('../config/db');

async function checkUserAchievements(user_id) {
  const client = await db.query('BEGIN');
  try {
    const userRes = await db.query('SELECT total_points FROM users WHERE id = $1', [user_id]);
    if (!userRes.rows[0]) throw new Error('Пользователь не найден');
    const points = userRes.rows[0].total_points;

    const res = await db.query(`
      SELECT a.id FROM achievements a
      LEFT JOIN user_achievements ua ON a.id = ua.achievement_id AND ua.user_id = $1
      WHERE a.required_points <= $2 AND ua.achievement_id IS NULL
    `, [user_id, points]);

    const newAchievements = res.rows;

    for (const ach of newAchievements) {
      await db.query(
        'INSERT INTO user_achievements (user_id, achievement_id) VALUES ($1, $2)',
        [user_id, ach.id]
      );
    }

    await db.query('COMMIT');
    return newAchievements;
  } catch (err) {
    await db.query('ROLLBACK');
    throw err;
  }
}

const Report = {
  getAll: async () => {
    const res = await db.query(`
      SELECT r.*, wt.name AS waste_type_name, u.username 
      FROM reports r
      JOIN waste_types wt ON r.waste_type_id = wt.id
      JOIN users u ON r.user_id = u.id
    `);
    return res.rows;
  },

  getById: async (id) => {
    const res = await db.query(`
      SELECT r.*, wt.name AS waste_type_name, u.username 
      FROM reports r
      JOIN waste_types wt ON r.waste_type_id = wt.id
      JOIN users u ON r.user_id = u.id
      WHERE r.id = $1
    `, [id]);
    return res.rows[0];
  },

  create: async (user_id, waste_type_id, weight_kg) => {
    const client = await db.query('BEGIN');

    try {
      if (
        typeof user_id !== 'number' ||
        typeof waste_type_id !== 'number' ||
        typeof weight_kg !== 'number'
      ) {
        throw new Error('Все параметры должны быть числами');
      }

      const userRes = await db.query('SELECT 1 FROM users WHERE id = $1', [user_id]);
      if (!userRes.rows[0]) {
        throw new Error('Пользователь не найден');
      }

      const wtRes = await db.query('SELECT eco_points FROM waste_types WHERE id = $1', [waste_type_id]);
      if (!wtRes.rows[0]) {
        throw new Error('Тип отхода не найден');
      }

      const eco_points = wtRes.rows[0].eco_points;
      const points_earned = Math.round(eco_points * weight_kg);

      const reportRes = await db.query(
        'INSERT INTO reports (user_id, waste_type_id, weight_kg, points_earned) VALUES ($1, $2, $3, $4) RETURNING *',
        [user_id, waste_type_id, weight_kg, points_earned]
      );

      await db.query(
        'UPDATE users SET total_points = total_points + $1 WHERE id = $2',
        [points_earned, user_id]
      );

      await checkUserAchievements(user_id);

      await db.query('COMMIT');

      return reportRes.rows[0];
    } catch (err) {
      await db.query('ROLLBACK');
      throw err;
    }
  },

  delete: async (id) => {
    const client = await db.query('BEGIN');

    try {
      const res = await db.query('SELECT * FROM reports WHERE id = $1', [id]);
      const report = res.rows[0];
      if (!report) throw new Error('Отчет не найден');

      await db.query(
        'UPDATE users SET total_points = GREATEST(total_points - $1, 0) WHERE id = $2',
        [report.points_earned, report.user_id]
      );

      await db.query('DELETE FROM reports WHERE id = $1', [id]);

      await db.query('COMMIT');
      return { message: 'Отчет удален' };
    } catch (err) {
      await db.query('ROLLBACK');
      throw err;
    }
  }
};

module.exports = Report;