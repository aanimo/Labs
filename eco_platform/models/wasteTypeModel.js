const db = require('../config/db');

const WasteType = {
  getAll: async () => {
    const res = await db.query('SELECT * FROM waste_types');
    return res.rows;
  },

  getById: async (id) => {
    const res = await db.query('SELECT * FROM waste_types WHERE id = $1', [id]);
    return res.rows[0];
  },

  create: async (name, eco_points) => {
    const res = await db.query(
      'INSERT INTO waste_types (name, eco_points) VALUES ($1, $2) RETURNING *',
      [name, eco_points]
    );
    return res.rows[0];
  },

  update: async (id, name, eco_points) => {
    const res = await db.query(
      'UPDATE waste_types SET name = $1, eco_points = $2 WHERE id = $3 RETURNING *',
      [name, eco_points, id]
    );
    return res.rows[0];
  },

  delete: async (id) => {
    const res = await db.query('DELETE FROM waste_types WHERE id = $1 RETURNING *', [id]);
    return res.rowCount > 0;
  }
};

module.exports = WasteType;