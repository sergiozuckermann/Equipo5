

async function getData(url) {
	try {
		const response = await fetch(url);
		const data = await response.json();
		// Do something with the response data
		return data;
	} catch (error) {
		console.error(error);
	}
}


async function classChart() {
	console.log("Prueba")
	const data = await getData("http://127.0.0.1:8000/api/class_election_stats");
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

function countToN(n) {
	const result = [];
	for (let i = 1; i <= n; i++) {
	  result.push(i);
	}
	return result;
  }


async function classChart2() {
	console.log("Prueba")
	const data = await getData("http://127.0.0.1:8000/api/damage_made_vs_received");
	console.log(data);
	var ctx = document.getElementById('apiChart2').getContext('2d');
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: countToN(data.damage_made.length),
			datasets: [{
				label: 'Damage Made',
				data: data.damage_made,
				borderColor: 'rgb(255, 99, 132)',
				tension: 0.1
			},
			{
				label: 'Damage Received',
				data: data.damage_received,
				borderColor: 'rgb(54, 162, 235)',
				tension: 0.1
			}]
		},
	});
}

async function classChart3() {
	console.log("Prueba")
	const data = await getData("http://127.0.0.1:8000/api/damage_made_vs_received");
	console.log(data);
	var ctx = document.getElementById('apiChart3').getContext('2d');
	var myChart = new Chart(ctx, {
		type: 'bar',
		data: {
			labels: ["Red", "Blue", "Yellow", "Green"],
			datasets: [{
				label: 'Player wins',
				data: [1,2,3,4],
				//randomize the color for each bar
				backgroundColor: 'rgb(255, 99, 132)',
			},
			{
				label: 'Enemy wins',
				data: [1,1.2,3.1,0.8],
				backgroundColor: 'rgb(54, 162, 235)',
			}]	
		},
	});
}


classChart();
classChart2();
classChart3();