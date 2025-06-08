const CollectionPoint = require('../models/collectionPointModel');

const getCollectionPoints = async (req, res) => {
  try {
    const points = await CollectionPoint.getAll();
    res.status(200).json(points);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const getCollectionPointById = async (req, res) => {
  const { id } = req.params;

  try {
    const point = await CollectionPoint.getById(id);
    if (!point) {
      return res.status(404).json({ error: 'Пункт приема не найден' });
    }
    res.status(200).json(point);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const createCollectionPoint = async (req, res) => {
  const { name, address, latitude, longitude } = req.body;

  if (!name || !address) {
    return res.status(400).json({
      error: 'Поля name и address обязательны'
    });
  }

  try {
    const newPoint = await CollectionPoint.create(name, address, latitude, longitude);
    res.status(201).json(newPoint);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const updateCollectionPoint = async (req, res) => {
  const { id } = req.params;
  const { name, address, latitude, longitude } = req.body;

  if (!name || !address) {
    return res.status(400).json({
      error: 'Поля name и address обязательны'
    });
  }

  try {
    const updatedPoint = await CollectionPoint.update(id, name, address, latitude, longitude);
    if (!updatedPoint) {
      return res.status(404).json({ error: 'Пункт приема не найден' });
    }
    res.status(200).json(updatedPoint);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const deleteCollectionPoint = async (req, res) => {
  const { id } = req.params;

  try {
    const deleted = await CollectionPoint.delete(id);
    if (!deleted) {
      return res.status(404).json({ error: 'Пункт приема не найден' });
    }
    res.status(200).json({ message: 'Пункт приема удален' });
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

module.exports = {
  getCollectionPoints,
  getCollectionPointById,
  createCollectionPoint,
  updateCollectionPoint,
  deleteCollectionPoint
};