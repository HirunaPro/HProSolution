using HPro.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HPro.Api.Data
{
    public class QlQuery
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ApplicationTag> GetApplicationTags([ScopedService] ApplicationDbContext context)
        {
            return context.ApplicationTags;
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ObjectTag> GetObjectTags([ScopedService] ApplicationDbContext context)
        {
            return context.ObjectTags;
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<List<ApplicationTag>> GetTagHierarchy([ScopedService] ApplicationDbContext context, Guid rootTagId)
        {
            return await context.ApplicationTags
                .Where(t => t.Id == rootTagId)
                .Include(t => t.ChildTags)
                .ToListAsync();
        }
    }

    public class Mutation
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<ApplicationTag> CreateTag([ScopedService] ApplicationDbContext context, string tagName, string tagType, Guid? parentTagId)
        {
            var tag = new ApplicationTag
            {
                TagName = tagName,
                TagType = tagType,
                ParentTagId = parentTagId
            };
            context.ApplicationTags.Add(tag);
            await context.SaveChangesAsync();
            return tag;
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<ObjectTag> TagObject([ScopedService] ApplicationDbContext context, Guid objectId, string objectType, Guid tagId)
        {
            var objectTag = new ObjectTag
            {
                ObjectId = objectId,
                ObjectType = objectType,
                Id = tagId
            };
            context.ObjectTags.Add(objectTag);
            await context.SaveChangesAsync();
            return objectTag;
        }
    }

    public class ApplicationTagType : ObjectType<ApplicationTag>
    {
        protected override void Configure(IObjectTypeDescriptor<ApplicationTag> descriptor)
        {
            descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
            descriptor.Field(t => t.TagName).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.TagType).Type<NonNullType<StringType>>();
            descriptor.Field(t => t.ParentTagId).Type<IdType>();
            descriptor.Field(t => t.ParentTag).Type<ApplicationTagType>().UseDbContext<ApplicationDbContext>()
                .ResolveWith<Resolvers>(r => r.GetParentTag(default!, default!));
            descriptor.Field(t => t.ChildTags).Type<ListType<ApplicationTagType>>().UseDbContext<ApplicationDbContext>()
                .ResolveWith<Resolvers>(r => r.GetChildTags(default!, default!));
        }

        private class Resolvers
        {
            public ApplicationTag GetParentTag([Parent] ApplicationTag tag, [ScopedService] ApplicationDbContext context)
            {
                return context.ApplicationTags.FirstOrDefault(t => t.Id == tag.ParentTagId);
            }

            public IQueryable<ApplicationTag> GetChildTags([Parent] ApplicationTag tag, [ScopedService] ApplicationDbContext context)
            {
                return context.ApplicationTags.Where(t => t.ParentTagId == tag.Id);
            }
        }
    }

    public class ObjectTagType : ObjectType<ObjectTag>
    {
        protected override void Configure(IObjectTypeDescriptor<ObjectTag> descriptor)
        {
            descriptor.Field(o => o.ObjectId).Type<NonNullType<IdType>>();
            descriptor.Field(o => o.ObjectType).Type<NonNullType<StringType>>();
            descriptor.Field(o => o.ApplicationTagId).Type<NonNullType<IdType>>();
            descriptor.Field(o => o.ApplicationTag).Type<ApplicationTagType>().UseDbContext<ApplicationDbContext>()
                .ResolveWith<Resolvers>(r => r.GetTag(default!, default!));
        }

        private class Resolvers
        {
            public ApplicationTag GetTag([Parent] ObjectTag objectTag, [ScopedService] ApplicationDbContext context)
            {
                return context.ApplicationTags.FirstOrDefault(t => t.Id == objectTag.ApplicationTagId);
            }
        }
    }

    //public class AppDbContext : DbContext
    //{
    //    public DbSet<ApplicationTag> ApplicationTags { get; set; }
    //    public DbSet<ObjectTag> ObjectTags { get; set; }
    //    // Other DbSets...
    //}
}
