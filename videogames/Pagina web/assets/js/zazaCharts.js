

async function getData() {
	try {
		const response = await fetch('http://127.0.0.1:8000/api/class_election_stats');
		const data = await response.json();
		// Do something with the response data
		return data;
	} catch (error) {
		console.error(error);
	}
}

async function classChart() {
	console.log("Prueba")
	const data = await getData();
	console.log(data);
	var ctx = document.getElementById('apiChart1').getContext('2d');

	var myChart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: data.labels,
			datasets: [{
				data: data.data,
				backgroundColor: [
					'rgba(255, 99, 132, 0.2)',
					'rgba(54, 162, 235, 0.2)',
					'rgba(255, 206, 86, 0.2)',
					'rgba(75, 192, 192, 0.2)',
					'rgba(153, 102, 255, 0.2)'
				],
				borderColor: [
					'rgba(255, 99, 132, 1)',
					'rgba(54, 162, 235, 1)',
					'rgba(255, 206, 86, 1)',
					'rgba(75, 192, 192, 1)',
					'rgba(153, 102, 255, 1)'
				],
				borderWidth: 1
			}]
		},
		options: {
			tooltips: {
				callbacks: {
					label: function (tooltipItem, data) {
						var dataset = data.datasets[tooltipItem.datasetIndex];
						var total = dataset.data.reduce(function (previousValue, currentValue, currentIndex, array) {
							return previousValue + currentValue;
						});
						var currentValue = dataset.data[tooltipItem.index];
						var percentage = Math.floor(((currentValue / total) * 100) + 0.5);
						return percentage + "%";
					}
				}
			}
		}
	});
}

classChart();