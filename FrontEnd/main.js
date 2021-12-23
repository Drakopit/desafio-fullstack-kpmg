// CONST
const URI = "https://localhost:44316/WeatherForecast"

window.onload = async function() {
    // Get app context
    var app = document.getElementById("app");
    
    // Received result from endpoint call
    var result = await simple_call();

    // Create table dynamic
    var table = app.querySelector("table");

    draw_table(table, result);
}

async function simple_call() {
    let response = await fetch(`${URI}`);
    let result = await response.json();
    return result;
}

async function draw_table(tbl, resultJSON) {
    for (let x = 0; x < resultJSON.length; x++) {
        // Create line
        let tr = tbl.insertRow();

        // Create cells
        {
            let td = tr.insertCell();
            td.appendChild(document.createTextNode(`${resultJSON[x].date}`));
            td = tr.insertCell();
            td.appendChild(document.createTextNode(`${resultJSON[x].summary}`));
            td = tr.insertCell();
            td.appendChild(document.createTextNode(`${resultJSON[x].temperatureC}`));
            td = tr.insertCell();
            td.appendChild(document.createTextNode(`${resultJSON[x].temperatureF}`));
        }
    }
}