function populateScores(playerId) {
  var scoresTable = $('#scores-table');

  $.ajax({
    type: "POST",
    url: '/Scores/PopulateScores',
    data: {
      playerId: playerId
    },
    /* Response is the data returned from controller method */
    success: function (response) {

      scoresTable.replaceWith(response);

      return false;
    },
    error: function () {
      alert('error')
    }
  });
}

$(function () {
  var playerId = $('#players-filter').val();
  populateScores(playerId);
});

$('#players-filter').change(function () {
  var playerId = $(this).val();

  populateScores(playerId);
});
