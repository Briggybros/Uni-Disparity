const fs = require('fs');

let eventCallbacks = {};
function triggerEvent(event, data) {
  const callbacks = eventCallbacks[event];
  if (callbacks && Array.isArray(callbacks)) {
    callbacks.forEach(callback => callback(data));
  }
}
function on(event, callback) {
  eventCallbacks = Object.assign({}, eventCallbacks, {
    [event]: eventCallbacks[event] ? [
      ...eventCallbacks[event],
      callback,
    ] : [callback],
  });
}

function loadScoreboard() {
  if (fs.existsSync('scores.json')) {
    const scoreboard = JSON.parse(fs.readFileSync('scores.json', { encoding: 'utf8' }));
    triggerEvent('load', { scoreboard });
    return scoreboard;
  }
  triggerEvent('load', { scoreboard: {} });
  return {};
}

let scoreboard = loadScoreboard();

function writeScoreboard() {
  const data = JSON.stringify(scoreboard);
  return new Promise((resolve, reject) => {
    fs.writeFile('scores.json', data, 'utf8', (err) => {
      if (err) {
        triggerEvent('failed-save');
        return reject(err);
      }
      triggerEvent('saved');
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
    triggerEvent('update', {
      level: score.level,
      name: score.name,
      time: score.time,
      rank: 1,
    });
  } else {
    while (
      scoreboard[score.level].length > index &&
      parseInt(scoreboard[score.level][index].time, 10) <= parseInt(score.time, 10)
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
    triggerEvent('update', {
      level: score.level,
      name: score.name,
      time: score.time,
      rank: index + 1,
    });
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

function count(level) {
  if (level) {
    if (scoreboard[level]) {
      return scoreboard[level].length;
    }
    return 0;
  }
  return Object.values(scoreboard).reduce((acc, board) => acc + board.length, 0);
}

module.exports = {
  on,
  writeScoreboard,
  insertScore,
  getScores,
  count,
};
