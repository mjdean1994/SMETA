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

    // Set ranges for domain and range first, or it won't work
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
    // Use variables for comprehension
    var sentiment_line = SentimentLine(x, y);
    var anger_line = AngerLine(x, y);
    var anticipation_line = AnticipationLine(x, y);
    var disgust_line = DisgustLine(x, y);
    var fear_line = FearLine(x, y);
    var joy_line = JoyLine(x, y);
    var sadness_line = SadnessLine(x, y);
    var surprise_line = SurpriseLine(x, y);
    var trust_line = TrustLine(x, y);

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
            addScatterPlot(svg, data, "sentiment", x, y);
            addScatterPlot(svg, data, "anger", x, y);
            addScatterPlot(svg, data, "anticipation", x, y);
            addScatterPlot(svg, data, "disgust", x, y);
            addScatterPlot(svg, data, "fear", x, y);
            addScatterPlot(svg, data, "joy", x, y);
            addScatterPlot(svg, data, "sadness", x, y);
            addScatterPlot(svg, data, "surprise", x, y);
            addScatterPlot(svg, data, "trust", x, y);
         

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

function SentimentLine(x, y) {
    return d3.svg.line()
        .x(function(d) { return x(d.date); })
        .y(function (d) { return y(d.sentiment); })
        .interpolate("monotone");
}

function AngerLine(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.anger); })
    .interpolate("monotone");
}

function AnticipationLine(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.anticipation); })
    .interpolate("monotone");
}

function DisgustLine(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.disgust); })
    .interpolate("monotone");
}

function FearLine(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.fear); })
    .interpolate("monotone");
}

function JoyLine(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.joy); })
    .interpolate("monotone");
}

function SadnessLine(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.sadness); })
    .interpolate("monotone");
}

function SurpriseLine(x, y) {
    return d3.svg.line()
    .x(function (d) { return x(d.date); })
    .y(function (d) { return y(d.surprise); })
    .interpolate("monotone");
}

function TrustLine(x, y) {
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


/*****************************************************************
 * 
 * Scatterplot element - Function that actually gets called
 * 
 * PARAMETERS:
 *   svg - the svg to append to
 *   data - the data
 *   attribute - for displaying in tooltips
 *   getAtt - function to get attribute value
 *
 ****************************************************************/
function addScatterPlot(svg, data, attribute, x, y) {

    // Add tooltip div, MAKE SIZE RESPONSIVE- NOT DONE
    var tooltip = d3.select("#graph")
        .append("div")
        .attr("class", "tooltip")
        .style("opacity", 0);

    //Format date/time - SEE TOOLTIPS
    var formatDate = d3.time.format("Date: %x \nTime: %H:%M%p");

    return svg.selectAll("dot")
        .data(data)
        .enter()
        .append("circle")
        .attr("id", "dataPlots")
        .attr("r", 3) //make it bigger when hovering?
        .attr("cx", function (d) { return x(d.date); })
        .attr("cy", function (d) {
            //if statements add to choose attribute
             return y(d.sentiment);
        })
        .on("mouseover", function (d) {

            tooltip.transition()
                .duration(250)
                .style("opacity", .9);

            //this bit right here should be different for responsive sizing
            tooltip.html(formatDate(d.date) + "<br/>" + attribute + ": " + d.sentiment)
                .style("left", (d3.event.pageX - 70) + "px")
                .style("top", (d3.event.pageY - 220) + "px");

        })
        .on("mouseout", function (d) {
            tooltip.transition()
                .duration(250)
                .style("opacity", 0);
        });
}

//if attribute == string, then use that function -> switch statements?

/***********************************************
 * 
 *            SUPPORT FUNCTIONS
 *
 ***********************************************/

