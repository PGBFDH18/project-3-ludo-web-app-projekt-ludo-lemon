(function () {
    'use strict';
    
    var bricks = document.getElementsByClassName("brick");
    var goBtn = document.getElementById("rollDiceButton");

    for (var i = 0; i < bricks.length; i++) {
        bricks[i].addEventListener("click", function (event) {
            var pieceClicked = event.target.innerHTML;

            console.log(event.target);
        });
    }

    goBtn.addEventListener("click", function () {
        var player = "Player" + getPlayer().toString()
        printPlayerTurn(player);
    })

    function getPlayer() {
        return Math.floor(Math.random() * 4) + 1;
    }

    function printPlayerTurn(player) {
        document.getElementById("currentPlayer").innerHTML = player;
    }

})();