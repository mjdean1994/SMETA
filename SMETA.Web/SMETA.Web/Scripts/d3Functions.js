function getGraph() {
    // dont hardcode data because fuck hardcoded data
    /* use v3 instead of v4, more documentation on it */

    // Set the dimensions of the canvas / graph
    var margin = { top: 20, right: 0, bottom: 30, left: 50 },
        width = 800 ,
        height = 385 - margin.top - margin.bottom;

    // Parse the date / time
    var parseDate = d3.time.format("%d-%b-%Y %H:%M").parse;

    //Format date/time - SEE TOOLTIPS
    var formatDate = d3.time.format("Date: %x \nTime: %H:%M%p");

    // Set range first, or it won't work
    var x = d3.time.scale().range([0, width]);
    var y = d3.scale.linear().range([height, 0]);

    // Define the axes
    var xAxis = d3.svg.axis()
        .scale(x)
        .orient("bottom")
        .ticks(5);

    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left")
        .ticks(5);

    // Define the line
    var sentiment_line = d3.svg.line()
        .x(function (d) { return x(d.date); })
        .y(function(d) { return y(d.sentiment); });

    // Add tooltip div, MAKE SIZE RESPONSIVE
    var tooltip = d3.select("#graph")
        .append("div")
        .attr("class", "tooltip")
        .style("opacity", 0);

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

    //check mongodb query results, != JSON format
    d3.json("../home/getdata",
        function (error, data) {
            data.forEach(function (d) {
                d.date = parseDate(d.date);
            });

            // Scale the range of the data
            x.domain(d3.extent(data, function (d) { return d.date; }));
            y.domain([0, d3.max(data, function(d) { return d.sentiment; })]);

            // Add data line
            svg.append("path")
                .attr("class", "line")
                .attr("d", sentiment_line(data));

            /* 
            * Scatterplot 
            * So tooltip can use data values
            */
            svg.selectAll("dot")
                .data(data)
                .enter()
                .append("circle")
                    .attr("r", 1) //make it bigger when hovering?
                    .attr("cx", function (d) { return x(d.date); })
                    .attr("cy", function(d) { return y(d.sentiment); })
                    .attr("id", "dataPlots")
                    // tooltip bit
                    .on("mouseover", function (d) {
                        tooltip.transition()
                            .duration(200)
                            .style("opacity", .9);
                       
                        tooltip.html(formatDate(d.date) + "<br/>Positivity: " + d.sentiment)
                            .style("left", (d3.event.pageX - 70) + "px")
                            .style("top", (d3.event.pageY - 220) + "px");
                        //make radius of circle bigger.
                    })
                    .on("mouseout", function (d) {
                        tooltip.transition()
                            .duration(250)
                            .style("opacity", 0);
                    });

            // Add the X Axis
            svg.append("g")
                .attr("transform", "translate(0," + height + ")")
                .call(xAxis);

            // Add the Y Axis
            svg.append("g")
                .call(yAxis);

        });
}

