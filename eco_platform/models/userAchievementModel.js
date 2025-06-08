const db = require('../config/db');

const UserAchievement = {
  getByUserId: async (userId) => {
    const res = await db.query(`
      SELECT a.id, a.title, a.required_points
      FROM user_achievements ua
      JOIN achievements a ON ua.achievement_id = a.id
      WHERE ua.user_id = $1
    `, [userId]);
    return res.rows;
  },

  hasAchievement: async (userId, achievementId) => {
    const res = await db.query(`
      SELECT 1 FROM user_achievements
      WHERE user_id = $1 AND achievement_id = $2
    `, [userId, achievementId]);
    return res.rowCount > 0;
  },

  addAchievementToUser: async (userId, achievementId) => {
    const exists = await UserAchievement.hasAchievement(userId, achievementId);
    if (exists) {
      return { message: 'Пользователь уже имеет это достижение' };
    }

    await db.query(`
      INSERT INTO user_achievements (user_id, achievement_id)
      VALUES ($1, $2)
    `, [userId, achievementId]);

    return { message: 'Достижение успешно добавлено' };
  }
};

module.exports = UserAchievement;