const db = require('../config/db');

const CollectionPoint = {
  getAll: async () => {
    const res = await db.query('SELECT * FROM collection_points');
    return res.rows;
  },

  getById: async (id) => {
    const res = await db.query('SELECT * FROM collection_points WHERE id = $1', [id]);
    return res.rows[0];
  },

  create: async (name, address, latitude, longitude) => {
    const res = await db.query(
      'INSERT INTO collection_points (name, address, latitude, longitude) VALUES ($1, $2, $3, $4) RETURNING *',
      [name, address, latitude, longitude]
    );
    return res.rows[0];
  },

  update: async (id, name, address, latitude, longitude) => {
    const res = await db.query(
      'UPDATE collection_points SET name = $1, address = $2, latitude = $3, longitude = $4 WHERE id = $5 RETURNING *',
      [name, address, latitude, longitude, id]
    );
    return res.rows[0];
  },

  delete: async (id) => {
    const res = await db.query('DELETE FROM collection_points WHERE id = $1 RETURNING *', [id]);
    return res.rowCount > 0;
  }
};

module.exports = CollectionPoint;