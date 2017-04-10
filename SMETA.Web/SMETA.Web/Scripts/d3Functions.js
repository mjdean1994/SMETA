/*
 * CREATES AND POPULATES GRAPH 
 */
function getGraph() {
    //CAPSTONE DOC DONT FORGET TO MENTION USE OF V3 INSTEAD OF V4 BC BETTER DOCUMENTATION

    // Remove existing graph, if applicable
    $("#graphBase").remove();
    $("#loading").removeClass("hidden");

    var startDate = $("#FilterAttr_StartDate").val();
    var endDate = $("#FilterAttr_EndDate").val();
    var query = $("#FilterAttr_Query").val();
    var interval = $("#FilterAttr_Interval").val();
    var username = $("#FilterAttr_Username").val();

    if (startDate == "" || startDate == null)
    {
        startDate = "2000-1-1";
    }

    if (endDate == "" || endDate == null)
    {
        endDate = "2050-12-31";
    }

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
        .innerTickSize(-height)
        .outerTickSize(0)
        .ticks(5);

    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left")
        .innerTickSize(-width)
        .outerTickSize(0)
        .ticks(5);

    // Define the line
    var PositiveLine = d3.svg.line()
        .x(function (d) { return x(d.date); })
        .y(function (d) { return y(d.positivity); });

    var NeutralLine = d3.svg.line()
        .x(function (d) { return x(d.date); })
        .y(function (d) { return y(d.neutrality); });

    // Adds the svg canvas
    var svg = d3.select("#graph")
        .append("svg")
        .attr("width", "95%")
        .attr("height", "100%")
        .attr("preserveAspectRatio", "xMinYMin meet")
        .attr("viewBox", "0 0 900 450")
        .attr("id","graphBase")
        .classed("svg-content-responsive", true)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")");



    d3.xhr("../home/getdata")
    .header("Content-Type", "application/json")
    .post(
        JSON.stringify({ startDate: startDate, endDate: endDate, query: query, interval: interval, username: username }),
        function (error, data) {
            var jsonData = JSON.parse(data.response);

            if (jsonData.length <= 0)
            {
                toastr.error("No results found with the selected filters.");
            }

            $.each(jsonData, function (index, value) {
                value.date = parseDate(value.date);
            })
            
            // Scale the domain and range of the data
            x.domain(d3.extent(jsonData, function (d) { return d.date; }));
            y.domain([getRangeMin(jsonData), getRangeMax(jsonData)]);

            // Add data line for each attribute
            drawLine(svg, NeutralLine, jsonData, "neutrality");
            drawLine(svg, PositiveLine, jsonData, "positivity");
         

            //* Add X Axis
            svg.append("g")
                .attr("transform", "translate(0," + height + ")")
                .call(xAxis);

            //* Add Y Axis
            svg.append("g")
                .call(yAxis);

            svg.append("g")
               .attr("class", "legend")
               .attr("transform", "translate(50,30)")
               .style("font-size", "12px")
               .call(d3.legend);

            $("#loading").addClass("hidden");
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