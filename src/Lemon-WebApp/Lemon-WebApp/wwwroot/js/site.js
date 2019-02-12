function showDiceDots(diceValue) {

    document.getElementById('center').style.display = 'none';
    document.getElementById('leftUpper').style.display = 'none';
    document.getElementById('leftMiddel').style.display = 'none';
    document.getElementById('leftLower').style.display = 'none';
    document.getElementById('rightUpper').style.display = 'none';
    document.getElementById('rightMiddel').style.display = 'none';
    document.getElementById('rightLower').style.display = 'none';

    if (diceValue == 1) {
        document.getElementById('center').style.display = 'inline';
    }
    if (diceValue == 2) {
        document.getElementById('leftUpper').style.display = 'inline';
        document.getElementById('rightLower').style.display = 'inline';
    }
    if (diceValue == 3) {
        document.getElementById('leftUpper').style.display = 'inline';
        document.getElementById('center').style.display = 'inline';
        document.getElementById('rightLower').style.display = 'inline';
    }
    if (diceValue == 4) {
        document.getElementById('leftUpper').style.display = 'inline';
        document.getElementById('leftLower').style.display = 'inline';
        document.getElementById('rightUpper').style.display = 'inline';
        document.getElementById('rightLower').style.display = 'inline';
    }
    if (diceValue == 5) {
        document.getElementById('center').style.display = 'inline';
        document.getElementById('leftUpper').style.display = 'inline';
        document.getElementById('leftLower').style.display = 'inline';
        document.getElementById('rightUpper').style.display = 'inline';
        document.getElementById('rightLower').style.display = 'inline';
    }
    if (diceValue == 6) {
        document.getElementById('leftUpper').style.display = 'inline';
        document.getElementById('leftMiddel').style.display = 'inline';
        document.getElementById('leftLower').style.display = 'inline';
        document.getElementById('rightUpper').style.display = 'inline';
        document.getElementById('rightMiddel').style.display = 'inline';
        document.getElementById('rightLower').style.display = 'inline';
    }
}

// ajax call to get dice value from WebApi
$('#rollDice').submit(function () { // catch the form's submit event
    $.ajax({ // create an AJAX call...
        url: "Ludo/RollDice", // the file to call
        type: "get", // GET or POST
        data: $("form").serialize(), // get the form data
        success: function (diceValue) { // on success..

            // method to show the markup of dice with diceValue
            showDiceDots(diceValue); 

            // moving mechanism
            $(document).ready(function () {
                $('.field').on('click', '.brick', function () {
                    $(this).appendTo('#' + diceValue);
                    var x = $(this).attr('id');
                    console.log(x);
                });
            });
        }
    });
    return false; // cancel original event to prevent form submitting
});


$('#create_game').submit(function () { // catch the form's submit event
    $.ajax({ // create an AJAX call...
        url: "Ludo/Something", // the file to call
        type: "POST", // GET or POST
        data: $("form").serialize(), // get the form data
        success: function () { // on success..
        }
    });
    return false; // cancel original event to prevent form submitting
});

