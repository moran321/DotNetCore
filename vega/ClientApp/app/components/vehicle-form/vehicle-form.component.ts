
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
  vehicle: any = {
    features: [],
    contact: {}
  };

  constructor(private vehicleService: VehicleService) {


  }

  ngOnInit() {
    this.makes = this.vehicleService.getMakes().subscribe(m => this.makes = m);
    this.features = this.vehicleService.getFeatures().subscribe(f => this.features = f);
  }

  onMakeChange() {
    var selectedMake = this.makes.find((m: any) => (m.id == this.vehicle.makeId));
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }



  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    }
    else {
      //remove it
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  onSaveClick() {
    console.log("save clicked");
  }

  submit(){
    this.vehicleService.create(this.vehicle)
    .subscribe(
      x=>console.log(x),
      err => {
        // if (err.status==400){

        // }
      });
  }

}
