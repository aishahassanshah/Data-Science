// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.12.2

using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace StudentBot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
       
        protected readonly ILogger Logger;
        private readonly string SchoolDialogID = "SchoolDlg";
        private readonly string SchoolRollNumberID = "SchoolRollNumberIDDlg";

        private readonly string EmailDialogID = "EmailDlg";
        //private readonly string PhoneDialogID = "PhoneDlg";
        private readonly string SocialMediaProfileDialogID = "SocialMediaProfileDlg";
        public string SocialMediaProfile { get; set; }

        // Dependency injection uses this constructor to instantiate MainDialog
        public MainDialog(ILogger<MainDialog> logger)
            : base(nameof(MainDialog))
        {

            Logger = logger;
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new TextPrompt(SchoolDialogID,SchoolNameValidation));
            AddDialog(new TextPrompt(SchoolRollNumberID,RollNumberValidation));

            AddDialog(new TextPrompt(EmailDialogID, EmailValidation));
            //AddDialog(new TextPrompt(PhoneDialogID, PhoneValidation));
            AddDialog(new TextPrompt(SocialMediaProfileDialogID, SocialMediaProfileValidation));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                BotNameStepAsync,
                UserNameStepAsync,
                UserSchoolNameStepAsync,
                UserBoardNameStepAsync,
                SchoolYearStepAsync,
                StudentClassStepAsync,
                StudentClassSectionStepAsync,
                UserRollNumberStepAsync,



                UserEmailStepAsync,
                SocialMediaProfileStepAsync,
                SocialMediaProfileLinkStepAsync,
                PersonalityStepAsync,
                AcademicStepAsync,
                HabitStepAsync,
                AspirationStepAsync,

                ConfirmStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> BotNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
            {
                Prompt = MessageFactory.Text("Hey Friend, I am your Study Bot. Can you give me a name?.")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> UserNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            
            stepContext.Values["BotName"] = (string)stepContext.Result;
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"{(string)stepContext.Values["BotName"]},says "), cancellationToken);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions
            {
                Prompt = MessageFactory.Text("Hello, Please enter your name.")
            }, cancellationToken);
        }


        private async Task<DialogTurnResult> UserSchoolNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            stepContext.Values["UserName"] = (string)stepContext.Result;
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Hi {(string)stepContext.Values["UserName"]},I am your Study Buddy named { (string)stepContext.Values["BotName"]}"), cancellationToken);

            return await stepContext.PromptAsync(SchoolDialogID, new PromptOptions
            {
                Prompt = MessageFactory.Text("What is your School Name?")
            }, cancellationToken);
        }


        private async Task<DialogTurnResult> UserBoardNameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            stepContext.Values["SchoolName"] = (string)stepContext.Result;
            List<string> UserBoardNameList = new List<string> { "Local Board", "IB" };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Please select the Board you are enrolled."), cancellationToken);
            return await CreateAdaptiveCardAsync(UserBoardNameList, stepContext, cancellationToken);
           
        }
     
        private async Task<DialogTurnResult> SchoolYearStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["BoardName"] = ((FoundChoice)stepContext.Result).Value;
            List<string> SchoolYearList = new List<string> { "2005","2006","2007","2008","2009","2010","2011","2012","2013","2014","2015","2016","2017","2018","2019","2020","2021","2022","2023" };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Please select the year you are enrolled."), cancellationToken);
            return await CreateAdaptiveCardAsync(SchoolYearList, stepContext, cancellationToken);
            
        }

        private async Task<DialogTurnResult> StudentClassStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["SchoolYear"] = ((FoundChoice)stepContext.Result).Value;
            List<string> StudentClassList = new List<string> { "3","4","5", "6", "7", "8", "9", "10", "11", "12"};
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Please select the Class you are enrolled."), cancellationToken);
            return await CreateAdaptiveCardAsync(StudentClassList, stepContext, cancellationToken);

        }

        private async Task<DialogTurnResult> StudentClassSectionStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["SchoolClass"] = ((FoundChoice)stepContext.Result).Value;
            List<string> StudentClassSectionList = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J","k","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z" };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Please select the Class Section you are enrolled."), cancellationToken);
            return await CreateAdaptiveCardAsync(StudentClassSectionList, stepContext, cancellationToken);

        }

       private async Task<DialogTurnResult> UserRollNumberStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            stepContext.Values["SchoolClassSection"] = ((FoundChoice)stepContext.Result).Value; 
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"{(string)stepContext.Values["BotName"]},says "), cancellationToken);
            return await stepContext.PromptAsync(SchoolRollNumberID, new PromptOptions
           
            {
               Prompt = MessageFactory.Text("What is your Roll-Number?")
            },
            
            cancellationToken);
        }

        /// /////////////////////////////////////////////////////////////////

        private async Task<DialogTurnResult> UserEmailStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["UserRollNumber"] = (string)stepContext.Result;
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"{(string)stepContext.Values["BotName"]},says "), cancellationToken);

            return await stepContext.PromptAsync(EmailDialogID, new PromptOptions
            {
                Prompt = MessageFactory.Text("Please enter your Email.")
            }, cancellationToken);
        }

        

        private async Task<DialogTurnResult> SocialMediaProfileStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["UserEmail"] = (string)stepContext.Result;
            List<string> socialMediaList = new List<string> { "Instagram", "Snapchat", "Facebook", "Twitter", "LinkedIn" };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Please select the Social Media Profile you use."), cancellationToken);
            return await CreateAdaptiveCardAsync(socialMediaList, stepContext, cancellationToken);
        }

        private async Task<DialogTurnResult> SocialMediaProfileLinkStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["SocialMediaProfile"] = ((FoundChoice)stepContext.Result).Value;
            SocialMediaProfile = (string)stepContext.Values["SocialMediaProfile"];
            return await stepContext.PromptAsync(SocialMediaProfileDialogID, new PromptOptions
            {
                Prompt = MessageFactory.Text($"Please enter the link of your {SocialMediaProfile} social media profile.")
            }, cancellationToken);
        }

        private async Task<DialogTurnResult> PersonalityStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["SocialMediaProfileLink"] = (string)stepContext.Result;
            List<string> personalChoiceList = new List<string> { "Like to Study in a group ?","Like to Study alone?", "Do you have a close circle of friends?", "Would you like to Study with a new people / new acquaintance?", "Would you like to Study with new acquaintance?" };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Tell us about your  Personal Academic  choices."), cancellationToken);
            return await CreateAdaptiveCardAsync(personalChoiceList, stepContext, cancellationToken);
        }

        private async Task<DialogTurnResult> AcademicStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["Personality"] = ((FoundChoice)stepContext.Result).Value;
            List<string> AcademicChoiceList = new List<string> { "Mathematics", "English", "Science","ICT" };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Tell us about your Academic Subjects."), cancellationToken);
            return await CreateAdaptiveCardAsync(AcademicChoiceList, stepContext, cancellationToken);
        }


        private async Task<DialogTurnResult> HabitStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["AcademicSubjects"] = ((FoundChoice)stepContext.Result).Value;
            List<string> HabitChoiceList = new List<string> { "More active in the morning", "More active in the morning", "Do you like solving problems, or reading text ? " };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Tell us about your Academic preferences and choices."), cancellationToken);
            return await CreateAdaptiveCardAsync(HabitChoiceList, stepContext, cancellationToken);
        }

        private async Task<DialogTurnResult> AspirationStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["Habits"] = ((FoundChoice)stepContext.Result).Value;
            List<string> AspirationChoiceList = new List<string> { "Doctor", "Engineer", "Scientist " };
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Tell us about your Aspirational Profession."), cancellationToken);
            return await CreateAdaptiveCardAsync(AspirationChoiceList, stepContext, cancellationToken);
        }


        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["AspirationProfession"] = ((FoundChoice)stepContext.Result).Value;
            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions
            {
                Prompt = MessageFactory.Text("Would you like to Confirm your details?")
            }, cancellationToken);
        }

        ///////////////////////////////////////////////////////////
        
        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                StudentDetail studentDetailsInfo = new StudentDetail()
                 {
                        BotName = (string)stepContext.Values["BotName"],
                        StudentName = (string)stepContext.Values["UserName"],
                        SchoolName = (string)stepContext.Values["SchoolName"],
                        BoardName = (string)stepContext.Values["BoardName"],
                        SchoolYear = (string)stepContext.Values["SchoolYear"],
                        SchoolClass = (string)stepContext.Values["SchoolClass"],
                        SchoolClassSection = (string)stepContext.Values["SchoolClassSection"],
                        UserRollNumber = (string)stepContext.Values["UserRollNumber"],

                        EmailAdderss = (string)stepContext.Values["UserEmail"],
                        SocialMediaProfile = (string)stepContext.Values["SocialMediaProfile"],
                        SocialMediaProfileLink = (string)stepContext.Values["SocialMediaProfileLink"],
                        Personality = (string)stepContext.Values["Personality"],
                        AcademicSubjects = (string)stepContext.Values["AcademicSubjects"],
                        Habits = (string)stepContext.Values["Habits"],
                        AspirationProfession = (string)stepContext.Values["AspirationProfession"],
                };
                


                //Use this JSON for your requirement.
                string stringjson = JsonConvert.SerializeObject(studentDetailsInfo);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Your details are stored, Thank you."), cancellationToken);
                #pragma warning disable CS4014
                // Because this call is not awaited, execution of the current method continues before the call is completed
                WriteAsync(stringjson);
                 // Because this call is not awaited, execution of the current method continues before the call is completed
                // using FileStream createStream = File.Create(@"C:\TEST\student.json");
                //System.IO.File.WriteAllText(@"C:\TEST\student.json", stringjson);
                //await stepContext.Context.SendActivityAsync(MessageFactory.Text("Your details are stored, Thank you."), cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Your details are not saved. Thank you."), cancellationToken);
            }
            // Restart the main dialog with a different message the second time around


            //var promptMessage = "What else can I do for you?";
            //return await stepContext.ReplaceDialogAsync(InitialDialogId, promptMessage, cancellationToken);
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }


        private async Task WriteAsync(string data)
        {
            string exePath =
    System.IO.Path.GetDirectoryName(
       System.Reflection.Assembly.GetEntryAssembly().Location);
            //  string apPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath
            using (var sw = new StreamWriter(exePath + "\\Student.json"))
            {
                await sw.WriteAsync(data);
            }
        }


        private async Task<bool> SchoolNameValidation(PromptValidatorContext<string> promptcontext, CancellationToken cancellationtoken)
        {
            string input = promptcontext.Recognized.Value;
            if (Regex.IsMatch(input, "^[a-zA-Z]+$"))
            {
                
                return true;
            }
            await promptcontext.Context.SendActivityAsync(" Please enter a valid School Name.",
                        cancellationToken: cancellationtoken);
            return false;
        }












        private async Task<bool> RollNumberValidation(PromptValidatorContext<string> promptcontext, CancellationToken cancellationtoken)
        {
            string number = promptcontext.Recognized.Value;
            if (Regex.IsMatch(number, @"^\d+$"))
            {
                int count = promptcontext.Recognized.Value.Length;
                if (count != 8)
                {
                    await promptcontext.Context.SendActivityAsync("Hello, you are missing some number !!!",
                        cancellationToken: cancellationtoken);
                    return false;
                }
                return true;
            }
            await promptcontext.Context.SendActivityAsync("The Roll number is not valid. Please enter a valid number.",
                        cancellationToken: cancellationtoken);
            return false;
        }





        private async Task<bool> EmailValidation(PromptValidatorContext<string> promptcontext, CancellationToken cancellationtoken)
        {
            string email = promptcontext.Recognized.Value;

            if (string.IsNullOrWhiteSpace(email))
            {
                await promptcontext.Context.SendActivityAsync("The email you entered is not valid, please enter a valid email.", cancellationToken: cancellationtoken);
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email)
                {
                    return true;
                }
                else
                {
                    await promptcontext.Context.SendActivityAsync("The email you entered is not valid, please enter a valid email.", cancellationToken: cancellationtoken);
                    return false;
                }
            }
            catch
            {
                await promptcontext.Context.SendActivityAsync("The email you entered is not valid, please enter a valid email.", cancellationToken: cancellationtoken);
                return false;
            }
        }

       
        private async Task<bool> SocialMediaProfileValidation(PromptValidatorContext<string> promptcontext, CancellationToken cancellationtoken)
        {
            string profileLink = promptcontext.Recognized.Value;

            if (profileLink.ToLower().Contains(SocialMediaProfile.ToLower()))
            {
                return true;
            }
            else
            {
                await promptcontext.Context.SendActivityAsync($"The profile link you entered for {SocialMediaProfile} is not valid. Please enter correct profile link.", cancellationToken: cancellationtoken);
                return false;
            }

        }

        private async Task<DialogTurnResult> CreateAdaptiveCardAsync(List<string> listOfOptions, WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0))
            {
                // Use LINQ to turn the choices into submit actions
                Actions = listOfOptions.Select(choice => new AdaptiveSubmitAction
                {
                    Title = choice,
                    Data = choice,  // This will be a string
                }).ToList<AdaptiveAction>(),
            };
            // Prompt
            return await stepContext.PromptAsync(nameof(ChoicePrompt), new PromptOptions
            {
                Prompt = (Activity)MessageFactory.Attachment(new Attachment
                {
                    ContentType = AdaptiveCard.ContentType,
                    // Convert the AdaptiveCard to a JObject
                    Content = JObject.FromObject(card),
                }),
                Choices = ChoiceFactory.ToChoices(listOfOptions),
                // Don't render the choices outside the card
                Style = ListStyle.None,
            },
                cancellationToken);
        }
    }
}

