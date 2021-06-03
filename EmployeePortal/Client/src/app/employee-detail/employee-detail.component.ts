import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

  constructor(private router:Router,private activeRoute:ActivatedRoute) { }

  ngOnInit() {
  }
  applyLeave(){
        this.router.navigate(['/applyleave'],{relativeTo:this.activeRoute})
  }

}
