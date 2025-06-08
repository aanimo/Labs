const UserAchievement = require('../models/userAchievementModel');

const addUserAchievement = async (req, res) => {
  const { userId, achievementId } = req.body;

  try {
    const result = await UserAchievement.addAchievementToUser(userId, achievementId);
    res.status(200).json(result);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const getUserAchievements = async (req, res) => {
  const { userId } = req.params;

  try {
    const achievements = await UserAchievement.getByUserId(userId);
    res.status(200).json(achievements);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

module.exports = {
  addUserAchievement,
  getUserAchievements
};