const fs = require('fs');

function loadScoreboard() {
  if (fs.existsSync('scores.json')) {
    return JSON.parse(fs.readFileSync('scores.json', { encoding: 'utf8' }));
  }
  return {};
}

let scoreboard = loadScoreboard();

function writeScoreboard() {
  const data = JSON.stringify(scoreboard);
  return new Promise((resolve, reject) => {
    fs.writeFile('scores.json', data, 'utf8', (err) => {
      if (err) return reject(err);
      return resolve();
    });
  });
}

function insertScore(score) {
  let index = 0;
  if (!scoreboard[score.level]) {
    scoreboard = Object.assign(
      {},
      scoreboard,
      {
        [score.level]: [{ name: score.name, time: score.time }],
      },
    );
  } else {
    while (
      scoreboard[score.level].length > index &&
    scoreboard[score.level][index].time <= score.time
    ) {
      index += 1;
    }

    scoreboard = Object.assign(
      {},
      scoreboard,
      {
        [score.level]: [
          ...scoreboard[score.level].slice(0, index),
          { name: score.name, time: score.time },
          ...scoreboard[score.level].slice(index, this.length),
        ],
      },
    );
  }

  writeScoreboard();

  return Object.assign({}, score, { rank: index + 1 });
}

function getScores(level, offset, size) {
  if (scoreboard[level]) {
    return scoreboard[level].map((item, idx) => Object.assign(
      {},
      item,
      {
        rank: idx + 1,
        level,
      },
    )).slice(offset, offset + size);
  }
  return [];
}

module.exports = {
  writeScoreboard,
  insertScore,
  getScores,
};
