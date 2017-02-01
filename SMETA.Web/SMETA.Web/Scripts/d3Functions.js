function getGraph() {
    // dont hardcode data because fuck hardcoded data
    /* use v3 instead of v4 */

    // Set the dimensions of the canvas / graph
    var margin = { top: 20, right: 0, bottom: 30, left: 50 },
        width = 800 ,
        height = 385 - margin.top - margin.bottom;

    // Parse the date / time
    var parseDate = d3.time.format("%d-%b-%y").parse;

    // Set range first, or it won't work
    var x = d3.time.scale().range([0, width]);
    var y = d3.scale.linear().range([height, 0]);

    // Define the axes
    var xAxis = d3.svg.axis()
        .scale(x)
        .orient("bottom")
        .ticks(10);

    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left")
        .ticks(5);

    // Define the line
    var sentiment_line = d3.svg.line()
        .x(function(d) { return x(d.date); })
        .y(function(d) { return y(d.positive); });

    // Adds the svg canvas
    var svg = d3.select("#graph")
        .append("svg")
        .attr("width", "95%")
        .attr("height", "100%")
        .attr("preserveAspectRatio", "xMinYMin meet")
        .attr("viewBox", "0 0 900 450")
        .classed("svg-content-responsive", true)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")");

    // Get the data because I don't know how to use hardcoded data
    d3.csv("../SampleData/data.csv",
        function(error, data) {
            data.forEach(function(d) {
                d.date = parseDate(d.date);
                d.positive = +d.positive;
            });

            // Scale the range of the data
            x.domain(d3.extent(data, function(d) { return d.date; }));
            y.domain([0, d3.max(data, function(d) { return d.positive; })]);

            // Add data line
            svg.append("path")
                .attr("class", "line")
                .attr("d", sentiment_line(data))
                .attr("id", "dataLine");


            // Add the X Axis
            svg.append("g")
                .attr("transform", "translate(0," + height + ")")
                .call(xAxis);

            // Add the Y Axis
            svg.append("g")
                .call(yAxis);

        });
}