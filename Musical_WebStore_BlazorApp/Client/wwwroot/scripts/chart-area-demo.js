
function CreateChart(values, dates){
var ctx = document.getElementById("myAreaChart");
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
}