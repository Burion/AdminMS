
function CreateChart(values, dates){
var ctx = document.getElementById("myAreaChart");
var bar = document.getElementById("barChart");
var radar = document.getElementById("radarChart");
new Chart(ctx, {
  type: 'line',
  data: {
    labels: dates,
    datasets: [{ 
        data: values,
        label: "Africa",
        borderColor: "#3e95cd",
        fill: false
      }
    ]
  },
  options: {
    title: {
      display: true,
      text: 'World population per region (in millions)'
    }
  }
});
new Chart(bar, {
  type: 'bar',
  data: {
    labels: dates,
    datasets: [{ 
        data: values,
        label: "Africa",
        borderColor: "#3e95cd",
        fill: false
      }
    ]
  },
  options: {
    title: {
      display: true,
      text: 'World population per region (in millions)'
    }
  }
});
new Chart(radar, {
  type: 'polarArea',
  data: {
    labels: dates,
    datasets: [{ 
        data: values,
        label: "Africa",
        backgroundColor: [
          "rgba(255, 0, 0, 0.5)",
          "rgba(100, 255, 0, 0.5)",
          "rgba(200, 50, 255, 0.5)",
          "rgba(0, 100, 255, 0.5)"
        ],
        fill: true
      }
    ]
  }
});
}