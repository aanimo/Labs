const Achievement = require('../models/achievementModel');

const getAchievements = async (req, res) => {
  try {
    const achievements = await Achievement.getAll();
    res.status(200).json(achievements);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const getAchievementById = async (req, res) => {
  const { id } = req.params;

  try {
    const achievement = await Achievement.getById(id);
    if (!achievement) {
      return res.status(404).json({ error: 'Достижение не найдено' });
    }
    res.status(200).json(achievement);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const createAchievement = async (req, res) => {
  const { title, description, required_points } = req.body;

  if (!title || required_points === undefined) {
    return res.status(400).json({ error: 'Поля title и required_points обязательны' });
  }

  try {
    const newAchievement = await Achievement.create(title, description, required_points);
    res.status(201).json(newAchievement);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const updateAchievement = async (req, res) => {
  const { id } = req.params;
  const { title, description, required_points } = req.body;

  if (!title || required_points === undefined) {
    return res.status(400).json({ error: 'Поля title и required_points обязательны' });
  }

  try {
    const updated = await Achievement.update(id, title, description, required_points);
    if (!updated) {
      return res.status(404).json({ error: 'Достижение не найдено' });
    }
    res.status(200).json(updated);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const deleteAchievement = async (req, res) => {
  const { id } = req.params;

  try {
    const deleted = await Achievement.delete(id);
    if (!deleted) {
      return res.status(404).json({ error: 'Достижение не найдено' });
    }
    res.status(200).json({ message: 'Достижение удалено' });
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

module.exports = {
  getAchievements,
  getAchievementById,
  createAchievement,
  updateAchievement,
  deleteAchievement
};