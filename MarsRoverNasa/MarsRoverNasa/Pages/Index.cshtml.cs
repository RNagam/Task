using MarsRover;
using MarsRoverNasa.FileUploadService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverNasa.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IFileUploadServie fileUploadService;

        public List<string> RoversState { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IFileUploadServie fileUploadService)
        {
            _logger = logger;
            this.fileUploadService = fileUploadService;
        }

        public void OnGet()
        {

        }

        public void OnPost(IFormFile file)
        {
            if (file != null)
            {
               string FilePath = fileUploadService.UploadFile(file);

                var commandCenter = GetCommandCenter();
                
                //Set Plataeu surface size
                commandCenter.SendCommand("5 5");

                RoversState = new List<string>();

                var lines = System.IO.File.ReadAllLines(FilePath);
               
                foreach (var line in lines)
                {
                    var roverData = line.Split('|');
                    foreach (var value in roverData)
                    {
                       string result = commandCenter.SendCommand(value);
                        if (result!=string.Empty && result!=null)
                        {                            
                            RoversState.Add(result);
                        }
                    }
                }
            }   
            
        }

        private CommandCenter GetCommandCenter()
        {
            var serviceProvider = new ServiceCollection()
               .AddSingleton<ILandingSurface, Plataeu>()
               .AddSingleton<IRoverSquadManager, RoverSquadManager>()
               .BuildServiceProvider();

            return new CommandCenter(serviceProvider);
        }
    }

}
