using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Infrastructure.Models.DTOs
{
    public class DogDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Color is required.")]
        public string? Color { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Weight must be a non-negative number.")]
        public int Weight { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Tail length must be a non-negative number.")]
        public int TailLength { get; set; }
    }
}
