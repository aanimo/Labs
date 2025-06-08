const Report = require('../models/reportModel');

const getReports = async (req, res) => {
  try {
    const reports = await Report.getAll();
    res.status(200).json(reports);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const getReportById = async (req, res) => {
  const { id } = req.params;

  try {
    const report = await Report.getById(id);
    if (!report) {
      return res.status(404).json({ error: 'Отчет не найден' });
    }
    res.status(200).json(report);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const createReport = async (req, res) => {
  const { user_id, waste_type_id, weight_kg } = req.body;

  if (!user_id || !waste_type_id || !weight_kg || weight_kg <= 0) {
    return res.status(400).json({
      error: 'user_id, waste_type_id и weight_kg обязательны и должны быть положительными'
    });
  }

  try {
    const newReport = await Report.create(user_id, waste_type_id, weight_kg);
    res.status(201).json(newReport);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

const deleteReport = async (req, res) => {
  const { id } = req.params;

  try {
    await Report.delete(id);
    res.status(200).json({ message: 'Отчет удален' });
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
};

module.exports = {
  getReports,
  getReportById,
  createReport,
  deleteReport
};