function populateScores(playerId) {
  var scoresTable = $('#scores-table');
  var url = $('#players-filter').attr("data-populate-scores-url");

  $.ajax({
    type: "POST",
    url: url,
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
