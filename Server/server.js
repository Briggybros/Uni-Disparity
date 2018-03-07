const express = require('express');
const bodyParser = require('body-parser');
const sqlite = require('sqlite3');

const PORT = 8090;

const app = express();
const db = new sqlite.Database('./database.sqlite');

db.serialize(() => {
  db.run('CREATE TABLE IF NOT EXISTS scores (name VARCHAR, score INT, completion_time INT)');
});

app.use(bodyParser.urlencoded({
  extended: false,
}));

app.post('/score', async (req, res) => {
  db.serialize(() => {
    const statement = db.prepare('INSERT INTO scores (name, score, completion_time) VALUES (?, ?, ?)');
    statement.run(req.body.name, req.body.score, req.body.time, (err) => {
      if (err) res.sendStatus(500);
      else res.sendStatus(200);
    });
  });
});

app.get('/score', async (req, res) => {
  db.serialize(() => {
    db.all('SELECT * FROM scores ORDER BY score DESC', (err, rows) => {
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
