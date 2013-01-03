$(function () {
  var arrow = $('#chain-arrow').html();
  $('.chain-visualizer > li:not(:last)').after('<li class="arrow">' + arrow + '</li>');

  $('.accordion-group ').click(function (event) {
    var element = $(event.currentTarget),
        sibling = element.next('div'),
        chainId = $('#chainId').val(),
        reportId = element.find('input').val();

    if (sibling.is(':visible') || $.data(sibling[0], 'loaded') === 'true') {
      sibling.slideToggle(250);
    }

    else {
      $.ajax({
        url: '/_fubu/instrumentation/request/details/' + chainId + '/' + reportId,
        type: 'GET',
        success: function (data) {
          sibling.slideToggle(250);
          sibling.html(data);
          $.data(sibling[0], 'loaded', 'true');
        }
      });
    }
  });
});