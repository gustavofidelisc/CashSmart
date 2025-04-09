import React from 'react';
import ReactApexChart from 'react-apexcharts';
import { ApexOptions } from 'apexcharts';

interface FinancialChartProps {
  receitas: number[];
  despesas: number[];
  saldos: number[];
}

export const GraficoBarras: React.FC<FinancialChartProps> = ({ receitas, despesas, saldos }) => {

  const options: ApexOptions = {
    chart: {
      type: 'bar',
      height: 500,
      toolbar: {
        show: true,
      },
    },
    plotOptions: {
      bar: {
        horizontal: false,
        columnWidth: '55%',
        borderRadius: 5,
        borderRadiusApplication: 'end',
      },
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      show: true,
      width: 2,
      colors: ['transparent'],
    },
    xaxis: {
      categories: [
        'Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
        'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'
      ],
      labels: {
        style: {
          fontSize: '12px',
        },
      },
    },
    yaxis: {
      title: {
        text: 'R$ (valores)',
      },
      labels: {
        formatter: function (value: number) {
          return `R$ ${value.toFixed(2)}`;
        },
      },
    },
    fill: {
      opacity: 1,
    },
    tooltip: {
      y: {
        formatter: function (val: number) {
          return `R$ ${val.toFixed(2)}`;
        },
      },
    },
    colors: ['#4CAF50', '#F44336', '#2196F3'],
    legend: {
      position: 'top',
      horizontalAlign: 'center',
    },
  };

  const series = [
    {
      name: 'Receitas',
      data: receitas,
    },
    {
      name: 'Despesas',
      data: despesas,
    },
    {
      name: 'Saldo Mensal',
      data: saldos,
    },
  ];

  return (
    <div id="financial-chart">
      <ReactApexChart
        options={options}
        series={series}
        type="bar"
        height={350}
      />
    </div>
  );
};

