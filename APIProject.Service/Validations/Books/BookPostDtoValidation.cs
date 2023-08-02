using APIProject.Service.Dtos.Books;
using APIProject.Service.Dtos.Categories;
using APIProject.Service.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Service.Validations.Categories
{
    public class BookPostDtoValidation : AbstractValidator<BookPostDto>
    {
        public BookPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(30);
            RuleFor(x => x.Author)
               .NotEmpty()
               .NotNull().WithMessage("Author can not be null")
               .MinimumLength(3)
               .MaximumLength(30);
            RuleFor(x => x)
            .Custom((x, context) =>
            {
                if (x.file != null)
                {
                    if (!Helper.isImage(x.file))
                    {
                        context.AddFailure("file", "The type of file must be image");
                    }
                    if (!Helper.isSizeOk(x.file, 2))
                    {
                        context.AddFailure("file", "The size of image less than 2 mb");
                    }
                }
            });
        }
    }
}
