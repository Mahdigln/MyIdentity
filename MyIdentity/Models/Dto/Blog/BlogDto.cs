﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyIdentity.Models.Dto.Blog;

public class BlogDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    [BindNever]
    public string UserId { get; set; }
    public string UserName { get; set; }
}

