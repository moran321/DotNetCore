
import { VehicleService } from '../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import 'rxjs/Subscription';
@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  //makes: any = [];
  makes: any = [];
  models: any = [];
  features: any = [];
  vehicle: any = {};

  constructor(private vehicleService: VehicleService) {


  }

  ngOnInit() {
    this.makes = this.vehicleService.getMakes().subscribe(m => this.makes = m);
    this.features = this.vehicleService.getFeatures().subscribe(f => this.features = f);
  }

  onMakeChange() {
    var selectedMake = this.makes.find((m: any) => (m.id == this.vehicle.make));
    this.models = selectedMake ? selectedMake.models : [];
  }

  onSaveClick() {
    console.log("save clicked");
  }
}
