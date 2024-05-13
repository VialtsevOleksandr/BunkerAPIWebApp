document.getElementById('startGame').addEventListener('click', function () {
    var playerCount = document.getElementById('playerCount').value;
    var playWithFriends = document.getElementById('playWithFriends').checked;

    if (!playerCount) {
        document.getElementById('error').classList.remove('hidden');
        document.getElementById('error').style.display = 'inline-block';
        typeError('Будь ласка, введіть кількість гравців.');
        return; 
    }

    var gameSettings = {
        CountOfPlayers: playerCount,
        PlayWithFriends: playWithFriends
    };

    document.getElementById('error').style.display = 'none';
    document.getElementById('error').textContent = ''; // Clear the error message

    fetch('api/GameSettings', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(gameSettings),
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(err => { throw err; });
            }
            return response.json();
        })
        .then(data => {
            document.getElementById('loading').classList.remove('hidden');
            setTimeout(function () {
                window.location.href = '/Game.html?id=' + data.id;
            }, 2000);
        })
        .catch((error) => {
            console.error('Error:', error);
            document.getElementById('error').classList.remove('hidden');
            document.getElementById('error').style.display = 'inline-block';
            typeError(error.message);
        });
});

function typeError(message) {
    var i = 0;
    var speed = 25; /* The speed/duration of the effect in milliseconds */

    function typeWriter() {
        if (i < message.length) {
            document.getElementById('error').innerHTML += message.charAt(i);
            i++;
            setTimeout(typeWriter, speed);
        }
    }

    typeWriter();
}
