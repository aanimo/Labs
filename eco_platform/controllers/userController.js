const User = require('../models/userModel');

const getUsers = async (req, res) => {
  try {
    const users = await User.getAll();
    res.status(200).json(users);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const getUserById = async (req, res) => {
  const { id } = req.params;

  try {
    const user = await User.getById(id);
    if (!user) {
      return res.status(404).json({ error: 'Пользователь не найден' });
    }
    res.status(200).json(user);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const createUser = async (req, res) => {
  const { username } = req.body;

  if (!username || username.trim() === '') {
    return res.status(400).json({ error: 'Имя пользователя обязательно' });
  }

  try {
    const newUser = await User.create(username);
    res.status(201).json(newUser);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const updateUser = async (req, res) => {
  const { id } = req.params;
  const { username } = req.body;

  if (!username || username.trim() === '') {
    return res.status(400).json({ error: 'Имя пользователя обязательно' });
  }

  try {
    const updatedUser = await User.update(id, username);
    if (!updatedUser) {
      return res.status(404).json({ error: 'Пользователь не найден' });
    }
    res.status(200).json(updatedUser);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const deleteUser = async (req, res) => {
  const { id } = req.params;

  try {
    const deleted = await User.delete(id);
    if (!deleted) {
      return res.status(404).json({ error: 'Пользователь не найден' });
    }
    res.status(200).json({ message: 'Пользователь удален' });
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

module.exports = {
  getUsers,
  getUserById,
  createUser,
  updateUser,
  deleteUser
};