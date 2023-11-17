import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js/auto'
import { OrderEntity } from 'src/app/entities/Order.Entity';
import { ProductEntity } from 'src/app/entities/Product.Entity';
import { ProductAttributeEntity } from 'src/app/entities/ProductAttribute.Entity';
import { UserAccountEntity } from 'src/app/entities/UserAccount.Entity';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  chart: Chart | any;
  total_price: any = 0;
  count_user: any;
  count_product: any;
  count_transaction: any;

  constructor(
    public orderService: BaseService<OrderEntity>,
    public userService: BaseService<UserAccountEntity>,
    public productService: BaseService<ProductAttributeEntity>,
  ) {
    this.getCountStatistic();
  }

  getCountStatistic() {
    this.orderService.getByRequest('Order', {}).subscribe(
      (res) => {
        this.count_transaction = res.data.length ?? 0;
      }
    );
    this.userService.getByRequest('UserAccount', {}).subscribe(
      (res) => {
        this.count_user = res.data.length ?? 0;
      }
    );
    this.productService.getByRequest('ProductAttribute', {}).subscribe(
      (res) => {
        this.count_product = res.data.length ?? 0;
      }
    );
  }

  ngOnInit(): void {
    this.createChart();
  }

  createChart() {
    this.chart = new Chart("UserAccountChart", {
      type: 'bar', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: ['2022-05-10', '2022-05-11', '2022-05-12', '2022-05-13',
          '2022-05-14', '2022-05-15', '2022-05-16', '2022-05-17',],
        datasets: [
          {
            label: "Sales",
            data: ['467', '576', '572', '79', '92',
              '574', '573', '576'],
            backgroundColor: 'blue'
          },
          {
            label: "Profit",
            data: ['542', '542', '536', '327', '17',
              '0.00', '538', '541'],
            backgroundColor: 'limegreen'
          }
        ]
      },
      options: {
        aspectRatio: 2.5
      }

    });

    this.chart = new Chart("ProductChart", {
      type: 'line', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: ['2022-05-10', '2022-05-11', '2022-05-12', '2022-05-13',
          '2022-05-14', '2022-05-15', '2022-05-16', '2022-05-17',],
        datasets: [
          {
            label: "Sales",
            data: ['467', '576', '572', '79', '92',
              '574', '573', '576'],
            backgroundColor: 'blue'
          },
          {
            label: "Profit",
            data: ['542', '542', '536', '327', '17',
              '0.00', '538', '541'],
            backgroundColor: 'limegreen'
          }
        ]
      },
      options: {
        aspectRatio: 2.5
      }

    });

    this.chart = new Chart("TransactionChart", {
      type: 'pie', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: ['2022-05-10', '2022-05-11', '2022-05-12', '2022-05-13',
          '2022-05-14', '2022-05-15', '2022-05-16', '2022-05-17',],
        datasets: [
          {
            label: "Sales",
            data: ['467', '576', '572', '79', '92',
              '574', '573', '576'],
            backgroundColor: 'blue'
          },
          {
            label: "Profit",
            data: ['542', '542', '536', '327', '17',
              '0.00', '538', '541'],
            backgroundColor: 'limegreen'
          }
        ]
      },
      options: {
        aspectRatio: 2.5
      }
    });
  }
}