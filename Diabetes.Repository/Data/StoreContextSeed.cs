using Diabetes.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diabetes.Repository.Data
{
    public static class StoreContextSeed
    {
        // Seeding
        public static async Task SeedAsync(StoreContext DbContext)
        {

            if (!DbContext.Patients.Any())
            {
                //Seeding Patients
                var PatientsData = File.ReadAllText("../Diabetes.Repository/Data/DataSeed/Patients.json");
                var Patients = JsonSerializer.Deserialize<List<Patient>>(PatientsData);
                if (Patients?.Count > 0)
                {
                    foreach (var Patient in Patients)
                    {
                        await DbContext.Set<Patient>().AddAsync(Patient);
                    }
                    await DbContext.SaveChangesAsync();

                }
            }
            if (!DbContext.ChatbotQuestionsDoctors.Any())
            {
                //Seeding ChatbotQuestionDoctors
                var ChatbotQuestionDoctorsData = File.ReadAllText("../Diabetes.Repository/Data/DataSeed/ChatbotQuestionDoctors.json");
                var ChatbotQuestionDoctors = JsonSerializer.Deserialize<List<ChatbotQuestionDoctor>>(ChatbotQuestionDoctorsData);
                if (ChatbotQuestionDoctors?.Count > 0)
                {
                    foreach (var ChatbotQuestionDoctor in ChatbotQuestionDoctors)
                    {
                        await DbContext.Set<ChatbotQuestionDoctor>().AddAsync(ChatbotQuestionDoctor);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }
            if (!DbContext.Posts.Any())
            {
                //Seeding Posts
                var PostsData = File.ReadAllText("../Diabetes.Repository/Data/DataSeed/Posts.json");
                var Posts = JsonSerializer.Deserialize<List<Post>>(PostsData);
                if (Posts?.Count > 0)
                {
                    foreach (var Post in Posts)
                    {
                        await DbContext.Set<Post>().AddAsync(Post);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }
            if (!DbContext.DiagnosisTypes.Any())
            {
                //Seeding DiagnosisTypes
                var DiagnosisTypesData = File.ReadAllText("../Diabetes.Repository/Data/DataSeed/DiagnosisTypes.json");
                var DiagnosisTypes = JsonSerializer.Deserialize<List<DiagnosisType>>(DiagnosisTypesData);
                if (DiagnosisTypes?.Count > 0)
                {
                    foreach (var DiagnosisType in DiagnosisTypes)
                    {
                        await DbContext.Set<DiagnosisType>().AddAsync(DiagnosisType);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }
            if (!DbContext.MedicalHistories.Any())
            {
                //Seeding MedicalHistories
                var MedicalHistoriesData = File.ReadAllText("../Diabetes.Repository/Data/DataSeed/MedicalHistories.json");
                var MedicalHistories = JsonSerializer.Deserialize<List<MedicalHistory>>(MedicalHistoriesData);
                if (MedicalHistories?.Count > 0)
                {
                    foreach (var medicalHistory in MedicalHistories)
                    {
                        await DbContext.Set<MedicalHistory>().AddAsync(medicalHistory);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }



        }
    }
}