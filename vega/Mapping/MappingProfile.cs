using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Models;

namespace vega.Mapping {
    public class MappingProfile : Profile {
        public MappingProfile () {
            //Domain to API resources
            CreateMap<Make, MakeResource> ();
            CreateMap<Make, KeyValPairResource> ();
          // CreateMap<Model, ModelResource> ();
           CreateMap<Model, KeyValPairResource> ();
            CreateMap<Feature, KeyValPairResource> ();
            CreateMap<Vehicle, SaveVehicleResource> ()
                .ForMember (vr => vr.Contact, opt => opt.MapFrom (v => new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember (vr => vr.Features, opt => opt.MapFrom (v => v.Features.Select (vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource> ()
                .ForMember (vr => vr.Make, opt => opt.MapFrom (v => v.Model.Make))
                .ForMember (vr => vr.Contact, opt => opt.MapFrom (v => new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember (vr => vr.Features, opt => opt.MapFrom (v => v.Features.Select (vf => new KeyValPairResource{Id=vf.Feature.Id, Name= vf.Feature.Name})));
            
            //API resources to Domain  (how to convert from client object to server)
            CreateMap<SaveVehicleResource, Vehicle> ().ForMember (v => v.ContactName, opt => opt.MapFrom (vr => vr.Contact.Name))
                .ForMember (v => v.Id, opt => opt.Ignore ())
                .ForMember (v => v.ContactEmail, opt => opt.MapFrom (vr => vr.Contact.Email))
                .ForMember (v => v.ContactPhone, opt => opt.MapFrom (vr => vr.Contact.Phone))
                .ForMember (v => v.Features, opt => opt.Ignore ())
                //.ForMember (v => v.Features, opt => opt.MapFrom (vr => vr.Features.Select (id => new VehicleFeature { FeatureId = id })));
                .AfterMap ((vr, v) => {
                    //Remove unselected features 
                    //remove features that exist in 'v' and not in 'vr'
                    var removedFeatures = v.Features.Where (f => !vr.Features.Contains (f.FeatureId)).ToList ();
                    foreach (var f in removedFeatures) {
                        v.Features.Remove (f);
                    }

                    //Add new features
                    //if 'vr' contains feature that not exist in 'v', add it to 'v'
                    var addedFeatures = vr.Features
                        .Where (id => !v.Features.Any (f => f.FeatureId == id))
                        .Select (id => (new VehicleFeature { FeatureId = id }))
                        .ToList ();
                    foreach (var f in addedFeatures) {
                        v.Features.Add (f);
                    }

                });
        }
    }
}