﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrphaCapital.Application.ViewModels;

namespace UrphaCapital.Application.UseCases.Selects.Queries
{
    public class SelectAllCoursesQuery : IRequest<IEnumerable<CourseSelectModel>>
    {
    }
}