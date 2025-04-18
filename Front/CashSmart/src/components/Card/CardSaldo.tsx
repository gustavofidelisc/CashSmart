import React from 'react';
import Card from 'react-bootstrap/Card';
import style from './CardSaldo.module.css';
interface ICardSaldoProps {
  title: string;
  icon: React.ReactNode;
  saldo: number;
}

export const CardSaldo: React.FC<ICardSaldoProps> = ({title, icon, saldo}) => {
  return (
    <Card className={style.cardContainer}>
      <Card.Body className={style.cardBody}>
        <Card.Title className={style.cardTitle}>
          <h3>
            {title}
          </h3>
          <div className={style.iconContainer}>
            {icon}  
          </div>
        </Card.Title>
        <Card.Text className={style.cardText}>
            <span>{`R$ ${saldo.toFixed(2)}`}</span>
        </Card.Text>
      </Card.Body>
    </Card>
  );
}

