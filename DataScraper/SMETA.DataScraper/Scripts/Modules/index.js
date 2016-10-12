var indexModule = {
    init: function() {
        // Declare a proxy to reference the hub. 
        var feed = $.connection.tweetHub;
        // Create a function that the hub can call to broadcast messages.
        feed.client.broadcastMessage = function (name, handle, message) {
            $('#tweetList').append('<li class="list-group-item"><strong>' + name
                + '</strong> (' + handle + ')<br/>' + message + '</li>');
            indexModule.count++;

            $("#tweetCount").text(indexModule.count);

            $("#tweetList").scrollTop($("#tweetList")[0].scrollHeight);
        };

        $.connection.hub.start().done(function () {
            // Nothing needed, we're only receiving
        });
    },
    count: 0
};