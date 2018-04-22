function query(name) {
  const url = window.location.href;
  const regex = new RegExp(`[?&]${name.replace(/[[\]]/g, '\\$&')}(=([^&#]*)|&|#|$)`);
  const results = regex.exec(url);
  if (!results) return null;
  if (!results[2]) return '';
  return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function createResult(result) {
  const row = document.createElement('div');
  row.className = 'border table-row';

  const content = document.createElement('div');
  content.className = 'table-row__content';
  row.appendChild(content);

  const rank = document.createElement('span');
  rank.innerHTML = `#${result.rank}`;
  content.appendChild(rank);

  const name = document.createElement('span');
  name.innerHTML = result.name;
  content.appendChild(name);

  const time = document.createElement('span');
  time.innerHTML = `${result.time} seconds`;
  content.appendChild(time);

  return row;
}

window.addEventListener('load', () => {
  const socket = io();

  socket.on('update', (results) => {
    console.log('Update', results);
    const DOMResults = document.getElementById('results');
    DOMResults.innerHTML = '';

    results.forEach((result) => {
      DOMResults.appendChild(createResult(result));
    });
  });

  function changeBoard(board, page) {
    socket.emit('board', board, page);
  }

  const board = query('board');
  const page = query('page');

  changeBoard(board, page);
});
