const db = require('../config/db');

const User = {
  getAll: async () => {
    const res = await db.query(`
      SELECT u.id, u.username, u.total_points, COUNT(r.id) AS reports_count
      FROM users u
      LEFT JOIN reports r ON u.id = r.user_id
      GROUP BY u.id
      ORDER BY u.id
    `);
    return res.rows;
  },

  getById: async (id) => {
    const res = await db.query(`
      SELECT u.id, u.username, u.total_points, COUNT(r.id) AS reports_count
      FROM users u
      LEFT JOIN reports r ON u.id = r.user_id
      WHERE u.id = $1
      GROUP BY u.id
    `);
    const user = res.rows[0];
    if (!user) return null;

    const achievementsRes = await db.query(`
      SELECT a.id, a.title, a.required_points
      FROM achievements a
      JOIN user_achievements ua ON a.id = ua.achievement_id
      WHERE ua.user_id = $1
    `, [id]);

    user.achievements = achievementsRes.rows;

    return user;
  },

  create: async (username) => {
    const res = await db.query(
      'INSERT INTO users (username) VALUES ($1) RETURNING *',
      [username]
    );
    return res.rows[0];
  },

  update: async (id, username) => {
    const res = await db.query(
      'UPDATE users SET username = $1 WHERE id = $2 RETURNING *',
      [username, id]
    );
    return res.rows[0];
  },

  delete: async (id) => {
    const res = await db.query('DELETE FROM users WHERE id = $1 RETURNING *', [id]);
    return res.rowCount > 0;
  }
};

module.exports = User;