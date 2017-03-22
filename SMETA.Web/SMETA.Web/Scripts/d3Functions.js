/*
 * CREATES AND POPULATES GRAPH 
 */
function getGraph() {
    //CAPSTONE DOC DONT FORGET TO MENTION USE OF V3 INSTEAD OF V4 BC BETTER DOCUMENTATION

    // Set the dimensions of the canvas / graph
    var margin = { top: 20, right: 0, bottom: 30, left: 50 },
        width = 800 ,
        height = 385 - margin.top - margin.bottom;

    // Parse the date / time
    var parseDate = d3.time.format("%d-%b-%Y %H:%M").parse;

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

    // Define the line
    var positive_line = d3.svg.line()
        .x(function (d) { return x(d.date); })
        .y(function (d) { return y(d.positivity); });

    var negative_line = d3.svg.line()
        .x(function (d) { return x(d.date); })
        .y(function (d) { return y(d.negativity); });

    var neutral_line = d3.svg.line()
        .x(function (d) { return x(d.date); })
        .y(function (d) { return y(d.neutrality); });

    // Add tooltip div
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


            //function: COMPARE MAX ATTR VALUES
            //MAKE FUNCTION FOR aLL 3 LINES COMPARE VALUE, GET GREATEST
            y.domain([getRangeMin(data), getRangeMax(data)]);

            // Add data line for each attribute
            drawLine(svg, neutral_line, data, "neutrality");
            drawLine(svg, positive_line, data, "positivity");
            drawLine(svg, negative_line, data, "negativity");

            /* 
            * So tooltip can use data values
            */
            addScatterPlot(svg, data, "Neutrality", x, y);
            addScatterPlot(svg, data, "Positivity", x, y);
            addScatterPlot(svg, data, "Negativity", x, y);
         

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



/***********************************************************
 *
 *            ------ SUPPORT FUNCTIONS -----
 *
 **********************************************************/

/*
 * Get the range for the graph
 */
function getRangeMax(data) {
    var maxPos = d3.max(data, function(d) { return d.positivity; });
    var maxNeu = d3.max(data, function(d) { return d.neutrality; });
    var maxNeg = d3.max(data, function(d) { return d.negativity; });

    if (maxPos >= maxNeu && maxPos >= maxNeg)
        return maxPos;
    else if (maxNeu >= maxPos && maxNeu >= maxNeg)
        return maxNeu;
    else if (maxNeg >= maxPos && maxNeg > maxNeu)
        return maxNeg;
    else {
        console.log("Unexpected output for getRangeMin");
    }
}

/*
 * Get the range for the graph
 */
function getRangeMin(data) {
    var minPos = d3.min(data, function (d) { return d.positivity; });
    var minNeu = d3.min(data, function (d) { return d.neutrality; });
    var minNeg = d3.min(data, function (d) { return d.negativity; });

    if (minPos <= minNeu && minPos <= minNeg)
        return minPos;
    else if (minNeu <= minPos && minNeu <= minNeg)
        return minNeu;
    else if (minNeg <= minPos && minNeg < minNeu)
        return minNeg;
    else {
        console.log("Unexpected output for getRangeMin");
    }
}


/*
 * Function to actually draw graphs
 */
function drawLine(svg, drawAtt, data, attribute) {
    svg.append("path")
        .attr("class", attribute)
        .attr("d", drawAtt(data));
}

/*
 * PARAMETERS:
 *   svg - the svg to append to
 *   data - the data
 *   attribute - for displaying in tooltips and determine attribute
 *   getAtt - function to get attribute value
 */
function addScatterPlot(svg, data, attribute, x, y) {

    // Add tooltip div, MAKE SIZE RESPONSIVE- NOT DONE
    var tooltip = d3.select("#graph")
        .append("div")
        .attr("class", "tooltip")
        .style("opacity", 0);

    //Format date/time - SEE TOOLTIPS
    var formatDate = d3.time.format("Date: %x <br/> Time: %H:%M%p");

    return svg.selectAll("dot")
        .data(data)
        .enter()
        .append("circle")
        .attr("id", "dataPlots")
        .attr("r", 1) //make it bigger when hovering?
        .attr("cx", function (d) { return x(d.date); })
        .attr("cy", function (d) {
            if (attribute == "Positivity")
                return y(d.positivity);
            else if (attribute == "Neutrality")
                return y(d.neutrality);
            else
                return y(d.negativity);
        })
        .on("mouseover", function (d) {

            tooltip.transition()
                .duration(250)
                .style("opacity", .9);

            tooltip.html(formatDate(d.date) + "<br/>" + attribute + ": " + d.positivity)
                .style("left", (d3.event.pageX - 70) + "px")
                .style("top", (d3.event.pageY - 220) + "px");

        })
        .on("mouseout", function (d) {
            tooltip.transition()
                .duration(300)
                .style("opacity", 0);
        });
}


function switchGraphData() {
    var elem = document.getElementById("graphView");

    if (elem.textContent == "Compound") {
        elem.textContent = "Split";
        //other things
    } else {
        elem.textContent = "Compound";
        //OTHER FUNCTION FOR RELOADING GRAPH
    }
}