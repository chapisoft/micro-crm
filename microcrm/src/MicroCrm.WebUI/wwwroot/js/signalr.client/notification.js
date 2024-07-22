"use strict";

//Object.defineProperty(WebSocket, 'OPEN', { value: 1, });
var connection = new signalR.HubConnectionBuilder().withUrl("/notificationhub").build();
connection.on("MicroCrm.eventbus", (eventData) => {
  console.log('subscribe MicroCrm.eventbus', eventData);
  toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": true,
    "progressBar": false,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": 300,
    "hideDuration": 100,
    "timeOut": 5000,
    "extendedTimeOut": 1000,
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
  }
  toastr["info"](`${eventData.content} ${eventData.from}`);
});
connection.start().catch(function (err) {
  return console.error(err.toString());
});
