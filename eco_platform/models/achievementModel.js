const db = require('../config/db');

const Achievement = {
  getAll: async () => {
    const res = await db.query('SELECT * FROM achievements');
    return res.rows;
  },

  getById: async (id) => {
    const res = await db.query('SELECT * FROM achievements WHERE id = $1', [id]);
    return res.rows[0];
  },

  create: async (title, description, required_points) => {
    const res = await db.query(
      'INSERT INTO achievements (title, description, required_points) VALUES ($1, $2, $3) RETURNING *',
      [title, description, required_points]
    );
    return res.rows[0];
  },

  update: async (id, title, description, required_points) => {
    const res = await db.query(
      'UPDATE achievements SET title = $1, description = $2, required_points = $3 WHERE id = $4 RETURNING *',
      [title, description, required_points, id]
    );
    return res.rows[0];
  },

  delete: async (id) => {
    const res = await db.query('DELETE FROM achievements WHERE id = $1 RETURNING *', [id]);
    return res.rowCount > 0;
  }
};

module.exports = Achievement;