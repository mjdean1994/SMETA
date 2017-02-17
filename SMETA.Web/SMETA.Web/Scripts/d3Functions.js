/*
 * CREATES AND POPULATES GRAPH 
 * break up into functions later
 */
function getGraph() {
    //CAPSTONE DOC DONT FORGET TO MENTION USE OF V3 INSTEAD OF V4 BC BETTER DOCUMENTATION
    //NOTE!!! variables can be used as functions depending on what theyre set to, ex. see parseDate

    // Set the dimensions of the canvas / graph
    var margin = { top: 20, right: 0, bottom: 30, left: 50 },
        width = 800 ,
        height = 385 - margin.top - margin.bottom;

    // Parse the date / time
    var parseDate = d3.time.format("%d-%b-%y").parse;

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

    // Add tooltip div, MAKE SIZE RESPONSIVE- NOT DONE
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


    // Define different attribute lines
    //use function conditionals for filter stuff later-just a note: HL
    var sentiment_line = GetSentiment(x, y); 
    var anger_line = GetAnger(x, y);
    var anticipation_line = GetAnticipation(x, y);
    var disgust_line = GetDisgust(x, y);
    var fear_line = GetFear(x, y);
    var joy_line = GetJoy(x, y);
    var sadness_line = GetSadness(x, y);
    var surprise_line = GetSurprise(x, y);
    var trust_line = GetTrust(x, y);

    //GetData will parse results and grab data with queries
    d3.json("Home/GetData",
        function (error, data) {
            data.forEach(function(d) {
                d.date = parseDate(d.date);
                d.sentiment = +d.sentiment;
                d.anger = +d.anger;
                d.anticipation = +d.anticipation;
                d.disgust = +d.disgust;
                d.fear = +d.fear;
                d.joy = +d.joy;
                d.sadness = +d.sadness;
                d.trust = +d.trust;
            });

            // Scale the range of the data
            x.domain(d3.extent(data, function (d) { return d.date; }));


            //function: COMPARE MAX ATTR VALUES
            //set default to max sentiment value of sample set -> 0.99
            y.domain([0, d3.max(data, function(d) { return d.sentiment; })]);

            // Add data line for each attribute
            drawLine(svg, sentiment_line, data, "sentiment");
            drawLine(svg, anger_line, data, "anger");
            drawLine(svg, anticipation_line, data, "anticipation");
            drawLine(svg, disgust_line, data, "disgust");
            drawLine(svg, fear_line, data, "fear");
            drawLine(svg, joy_line, data, "joy");
            drawLine(svg, sadness_line, data, "sadness");
            drawLine(svg, surprise_line, data, "surprise");
            drawLine(svg, trust_line, data, "trust");

            /* 
            * Scatterplot JUST FOR SENTIMENT FOR NOW
            * So tooltip can use data values
            */
            svg.selectAll("dot")
                .data(data)
                .enter()
                .append("circle")
                .attr("r", 3) //make it bigger when hovering?
                .attr("cx", function(d) { return x(d.date); })
                .attr("cy", function(d) { return y(d.sentiment); })
                .attr("id", "dataPlots")
                .on("mouseover", function (d) {

                    /*changes all dots, needs to be individual
                    dot.transition()
                        .duration(1000)
                        .attr("r", 6);*/

                    tooltip.transition()
                        .duration(250)
                        .style("opacity", .9);
                       
                    tooltip.html(formatDate(d.date) + "<br/>Sentiment: " + d.sentiment)
                        .style("left", (d3.event.pageX - 70) + "px")
                        .style("top", (d3.event.pageY - 220) + "px");

                })
                .on("mouseout", function (d) {
                    tooltip.transition()
                        .duration(250)
                        .style("opacity", 0);
                });

            //* Add X Axis
            svg.append("g")
                .attr("transform", "translate(0," + height + ")")
                .call(xAxis);

            //* Add Y Axis
            svg.append("g")
                .call(yAxis);

            // Legends are above on dashboard, change colors

        });
}







/*
* GETATTRIBUTE FUNCTION RETURN FOR SIMPLICITY
* Way to simplify it somehow but after functionality
*/

function GetSentiment(x, y) {
    return d3.svg.line()
        .x(function(d) { return x(d.date); })
        .y(function (d) { return y(d.sentiment); })
        .interpolate("monotone");
}

function GetAnger(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.anger); })
    .interpolate("monotone");
}

function GetAnticipation(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.anticipation); })
    .interpolate("monotone");
}

function GetDisgust(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.disgust); })
    .interpolate("monotone");
}

function GetFear(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.fear); })
    .interpolate("monotone");
}

function GetJoy(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.joy); })
    .interpolate("monotone");
}

function GetSadness(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.sadness); })
    .interpolate("monotone");
}

function GetSurprise(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.surprise); })
    .interpolate("monotone");
}

function GetTrust(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.trust); })
    .interpolate("monotone");
}


/*
 * Function to actually draw graphs
 * need svg
 *
 */
function drawLine(svg, drawAtt, data, attribute) {
    svg.append("path")
        .attr("class", attribute)
        .attr("d", drawAtt(data));
}