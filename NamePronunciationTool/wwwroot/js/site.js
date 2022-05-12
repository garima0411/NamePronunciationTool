Highcharts.chart('container', {
    chart: {
        type: 'line',
        width: 750,
        height: 220,
        style: {
            fontFamily: 'Helvetica',
            color: '#4a4a4a'
        }
    },
    legend: {
        enabled: true,
        align: 'right',
        verticalAlign: 'middle',
        layout: 'vertical',
        itemStyle: {
            fontWeight: 'normal'
        }
    },
    title: {
        text: ' '
    },
    credits: {
        enabled: false
    },
    subtitle: {
        text: ' '
    },
    xAxis: {
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        lineWidth: 0
    },
    yAxis: {
        title: {
            text: ' '
        },
        tickInterval: 5
    },
    plotOptions: {
        line: {
            dataLabels: {
                enabled: false
            }
        }
    },
    series: [{
        name: 'Range',
        color: '#d9d9d9',
        fillOpacity: 0.3,
        lineWidth: 0,
        type: 'arearange',
        showInLegend: false,
        marker: {
            enabled: false
        },
        data: [
            ['Jan', 60, 64],
            ['Feb', 62, 65],
            ['Mar', 60, 63],
            ['Apr', 61, 65],
            ['May', 63, 65],
            ['Jun', 64, 67],
            ['Jul', 63, 66],
            ['Aug', 65, 67],
            ['Sep', 63, 67],
            ['Oct', 62, 66],
            ['Nov', 63, 65],
            ['Dec', 64, 66]
        ]
    }, {
        name: 'Warning',
        showInLegend: false,
        color: '#f68d2e',
        fillOpacity: 0.3,
        type: 'arearange',
        lineWidth: 0,
        marker: {
            enabled: false
        },
        data: [
            ['Jan', 59, 60],
            ['Feb', 61, 62],
            ['Mar', 59, 60],
            ['Apr', 60, 61],
            ['May', 62, 63],
            ['Jun', 63, 64],
            ['Jul', 62, 63],
            ['Aug', 64, 65],
            ['Sep', 62, 63],
            ['Oct', 61, 62],
            ['Nov', 62, 63],
            ['Dec', 63, 64]
        ]
    }, {
        name: 'London',
        color: '#172e4d',
        type: 'line',
        marker: {
            enabled: false
        },
        data: [64, 65, 63, 65, 65, 67, 66, 67, 67, 66, 65, 66]
    }, {
        name: 'America',
        color: '#F72F2F',
        type: 'line',
        marker: {
            enabled: false
        },
        data: [59, 61, 59, 60, 62, 63, 62, 64, 62, 61, 62, 63]
    }, {
        name: 'Avergae Sample',
        color: '#005EB8',
        data: [61, 62, 58, 59, 67, 66, 60, 68, 63, 65, 60, 67],
        marker: {
            symbol: 'circle',
            radius: 3,
            lineWidth: 2,
            fillColor: '#fff',
            lineColor: '#005EB8'
        }
    }]
});