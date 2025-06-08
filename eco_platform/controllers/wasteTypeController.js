const WasteType = require('../models/wasteTypeModel');

const getWasteTypes = async (req, res) => {
  try {
    const types = await WasteType.getAll();
    res.status(200).json(types);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const getWasteTypeById = async (req, res) => {
  const { id } = req.params;

  try {
    const type = await WasteType.getById(id);
    if (!type) {
      return res.status(404).json({ error: 'Тип отхода не найден' });
    }
    res.status(200).json(type);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const createWasteType = async (req, res) => {
  const { name, eco_points } = req.body;

  if (!name || eco_points === undefined) {
    return res.status(400).json({ error: 'Name и eco_points обязательны' });
  }

  try {
    const newType = await WasteType.create(name, eco_points);
    res.status(201).json(newType);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const updateWasteType = async (req, res) => {
  const { id } = req.params;
  const { name, eco_points } = req.body;

  if (!name || eco_points === undefined) {
    return res.status(400).json({ error: 'Name и eco_points обязательны' });
  }

  try {
    const updatedType = await WasteType.update(id, name, eco_points);
    if (!updatedType) {
      return res.status(404).json({ error: 'Тип отхода не найден' });
    }
    res.status(200).json(updatedType);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const deleteWasteType = async (req, res) => {
  const { id } = req.params;

  try {
    const deleted = await WasteType.delete(id);
    if (!deleted) {
      return res.status(404).json({ error: 'Тип отхода не найден' });
    }
    res.status(200).json({ message: 'Тип отхода удален' });
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

module.exports = {
  getWasteTypes,
  getWasteTypeById,
  createWasteType,
  updateWasteType,
  deleteWasteType
};