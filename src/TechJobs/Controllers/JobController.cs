﻿using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using System;
using System.Collections.Generic;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        //TODO # 1
        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            
            Job someJob = jobData.Find(id);

            return View(someJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {

            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationsID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetenciesID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypesID),

                    //newJobViewModel. gives access to the view model with all the IDs
                };
                // the jobcontroller connects to the JobData function of Find
                //  think how the DataImporter works with the Job fields
                jobData.Jobs.Add(newJob);
                //add job to jobdata using newJob variable
                // return with id
                return Redirect("/Job?id=" + newJob.ID.ToString());

            }
            //else
            //{
            //    newJobViewModel.Jobs = jobData.FindByColumnAndValue(newJobViewModel.Column, newJobViewModel.Value);
            //}

            //newJobViewModel.Title = "Search";

            //;

            //TODO #6 - Validate the ViewModel and if valid, create a 
            //new Job and add it to the JobData data store.Then
            //redirect to the Job detail(Index) action/view for the new Job.

            //return View("Index");
            return View(newJobViewModel);
        }
    }
}
