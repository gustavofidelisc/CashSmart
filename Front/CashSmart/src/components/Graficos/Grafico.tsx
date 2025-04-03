import React from 'react';
import ReactApexChart from 'react-apexcharts';
import ReactDOM from 'react-dom';

// Interfaces de tipo
interface ChartOptions {
  chart: {
    width: number;
    type: 'pie' | 'donut' | 'bar' | 'line'; 
  };
  labels?: string[];
  responsive?: {
    breakpoint: number;
    options: {
      chart: {
        width: number;
      };
      legend: {
        position: 'top' | 'right' | 'bottom' | 'left';
      };
    };
  }[];
  legend?: {
    position?: 'top' | 'right' | 'bottom' | 'left';
    [key: string]: any; // Para outras propriedades opcionais do legend
  };
  // Adicione outras propriedades de options conforme necessário
}

interface ChartState {
  series: number[];
  options: ChartOptions;
}

interface ChartProps {
  series: number[];
  labels: string[];
}

export const Grafico: React.FC<ChartProps>= ({series, labels}) => {
  const [state] = React.useState<ChartState>({
    series: series,
    options: {
      chart: {
        width: 380,
        type: 'pie',
      },
      labels: labels,
      responsive: [{
        breakpoint: 480,
        options: {
          chart: {
            width: 350
          },
          legend: {
            position: 'right'
          }
        }
      }]
    },
  });

  return (
    <div>
      <div id="chart">
        <ReactApexChart 
          options={state.options} 
          series={state.series} 
          type={state.options.chart.type} 
          width={state.options.chart.width} 
        />
      </div>
      <div id="html-dist"></div>
    </div>
  );
};

