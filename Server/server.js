const express = require('express');
const bodyParser = require('body-parser');
const sqlite = require('sqlite3');

const PORT = 8090;

const app = express();
const db = new sqlite.Database('./database.sqlite');

db.serialize(() => {
  db.run('CREATE TABLE IF NOT EXISTS scores (name VARCHAR, level VARCHAR, completion_time INT)');
});

app.use(bodyParser.json());

app.post('/score', async (req, res) => {
  db.serialize(() => {
    const statement = db.prepare('INSERT INTO scores (name, level, completion_time) VALUES (?, ?, ?)');
    statement.run(req.body.name, req.body.level, req.body.time, (err) => {
      if (err) res.sendStatus(500);
      else res.sendStatus(200);
    });
  });
});

app.get('/score', async (req, res) => {
  db.serialize(() => {
    const statement = db.prepare(`SELECT name, completion_time FROM scores ORDER BY score DESC ${req.query.level ? 'WHERE level=?' : ''}`);
    statement.all(req.body.level, (err, rows) => {
      if (err) res.sendStatus(500);
      else {
        const { count } = req.query;
        const results = rows.slice(0, count || rows.length);
        res.status(200).send(JSON.stringify(results));
      }
    });
  });
});

app.listen(PORT, () => console.log(`Server listening on port: ${PORT}`));
