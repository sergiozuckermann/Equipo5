

async function getData(ep) {
	try {
		const response = await fetch("http://127.0.0.1:6000/api/" + ep);
		const data = await response.json();
		// Do something with the response data
		return data;
	} catch (error) {
		console.error(error);
	}
}


async function classChart() {

	const data = await getData("class_election_stats");
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


async function damageChart() {

	const data = await getData("damage_made_vs_received");

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

async function enemyChart() {
	const data = await getData("enemy_win_rate");
	var ctx = document.getElementById('apiChart3').getContext('2d');
	var myChart = new Chart(ctx, {
		type: 'bar',
		data: {
			labels: data.labels,
			datasets: [{
				label: 'Player wins',
				data: data.enemyloses,
				//randomize the color for each bar
				backgroundColor: 'rgb(255, 99, 132)',
			},
			{
				label: 'Enemy wins',
				data: data.enemywins,
				backgroundColor: 'rgb(54, 162, 235)',
			}]
		},
	});
}

async function attacksChart() {
	const data = await getData("attack_uses");
	const timesArray = data.map(({ times }) => times);
	const attackArray = data.map(({ attack }) => attack);

	var ctx = document.getElementById('apiChart4').getContext('2d');

	var myChart = new Chart(ctx, {
		type: 'pie',
		data: {
			labels: attackArray,
			datasets: [{
				data: timesArray,
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

async function criticalMissChart() {
	const data = await getData("criticals_vs_missed");
	var ctx = document.getElementById('apiChart5').getContext('2d');
	var myChart = new Chart(ctx, {
		type: 'bar',
		data: {
			labels: ["Attack Missed", "Critical Attacks"]
			,
			datasets: [{
				label: 'Player wins',
				data: [data[0].attacks_missed_lost, data[0].critical_attacks_lost],
				//randomize the color for each bar
				backgroundColor: 'rgb(255, 99, 132)',
			},
			{
				label: 'Player losses',
				data: [data[0].attacks_missed_won, data[0].critical_attacks_won],
				backgroundColor: 'rgb(54, 162, 235)',
			}]
		},
	});
}

classChart();
damageChart();
enemyChart();
attacksChart();
criticalMissChart();