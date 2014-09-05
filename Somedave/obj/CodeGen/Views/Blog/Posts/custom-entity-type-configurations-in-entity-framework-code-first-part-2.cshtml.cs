#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Somedave.Views.Blog.Posts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using FluentBootstrap;
    using Somedave;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Blog/Posts/custom-entity-type-configurations-in-entity-framework-code-fir" +
        "st-part-2.cshtml")]
    public partial class custom_entity_type_configurations_in_entity_framework_code_first_part_2 : Somedave.BlogPostViewPage<dynamic>
    {
        public custom_entity_type_configurations_in_entity_framework_code_first_part_2()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Blog\Posts\custom-entity-type-configurations-in-entity-framework-code-first-part-2.cshtml"
  
    Title = "Custom Entity Type Configurations in Entity Framework Code First (Part 2)";
    Published = new DateTime(2013, 5, 13);
    Tags = new[] { "Entity Framework", "Entity Framework Code First" };

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<p>In ");

            
            #line 7 "..\..\Views\Blog\Posts\custom-entity-type-configurations-in-entity-framework-code-first-part-2.cshtml"
 Write(Html.Post("my last post", x => x.custom_entity_type_configurations_in_entity_framework_code_first_part_1));

            
            #line default
            #line hidden
WriteLiteral(@" I discussed how to inherit from the <code>EntityTypeConfiguration</code> class and use reflection to dynamically configure Entity Framework. In this post I'll expand on that technique by using a custom interface, reflection, and several helper classes to automatically apply Entity Framework configurations from arbitrary classes.</p>

<p>The first step is to revisit the <code><a");

WriteLiteral(" href=\"http://msdn.microsoft.com/en-us/library/gg696117(v=vs.103).aspx\"");

WriteLiteral(">EntityTypeConfiguration&lt;TEntityType&gt;</a></code> that is part of Entity Fra" +
"mework. Recall that by overriding it you can provide your own configurations:</p" +
">\r\n\r\n<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">public class DepartmentTypeConfiguration : EntityTypeConfiguration&lt;Department&gt;
{
  public DepartmentTypeConfiguration()
  {
    Property(t =&gt; t.Name).IsRequired();
  }
}</code></pre>

<p>Once you're done, the <code>EntityTypeConfiguration</code> class is registered with the <code><a");

WriteLiteral(" href=\"http://msdn.microsoft.com/en-us/library/system.data.entity.modelconfigurat" +
"ion.configuration.configurationregistrar(v=vs.103).aspx\"");

WriteLiteral(@">ConfigurationRegistrar</a></code>. One problem with this is that the <code>ConfigurationRegistrar</code> only accepts one <cdoe>EntityTypeConfiguration</cdoe> per type. To get around this, instead of creating derived <cdoe>EntityTypeConfiguration</cdoe> classes we're going to create a custom interface that can be implemented to apply configuration to a common pre-generated <code>EntityTypeConfiguration</code> class per type. This way, even if there are multiple configurations for a given entity type, they'll all get applied to the same <code>EntityTypeConfiguration</code> instance. The first step is defining the interface that our own configuration classes will implement:</p>

<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">// This interface is needed to refer to the closed generic without the generic
// type parameter being available
public interface IEntityTypeConfiguration
{
}

// Implement this interface to enable dynamic entity configuration 
public interface IEntityTypeConfiguration&lt;TEntity&gt;
  : IEntityTypeConfiguration where TEntity : class
{
  void Configure(EntityTypeConfiguration&lt;TEntity&gt; configuration);
}</code></pre>

<p>The first non-generic version of the interface is only needed so that we can refer to the implementation without requiring the generic type parameter to be specified. The generic interface is the one that should be implemented to provide entity configuration data. Notice how instead of accessing an <code>EntityTypeConfiguration</code> by inheriting from it, we are now passing it in through the <code>Configure()</code> method. This results in configuration code that looks like:</p>

<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">public class Department 
{

  // ... Department entity data goes here

  // This is our entity configuration code
  // I prefer to use a nested class to keep everything together, but you don't have to
  public class DepartmentTypeConfiguration : IEntityTypeConfiguration&lt;Department&gt;
  {
    void Configure(EntityTypeConfiguration&lt;Department&gt; configuration)
    {
      configuration.Property(t =&gt; t.Name).IsRequired();
    }
  }
}</code></pre>

<p>The rest of the code that we need is all just rigging code to get this technique to work. The first helper class we'll need allows us to add an <code>EntityTypeConfiguration</code> to the <code>ConfigurationRegistrar</code> without knowing it's generic parameter types. This is needed because <code>ConfigurationRegistrar.Add&lt;TEntity&gt;()</code> is a generic method and otherwise wouldn't let us add an open generic instance. If this isn't making much sense yet, hang in there. Hopefully it will by the time I'm done.</p>

<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">// Similar to above, this lets us call AddConfiguration() without knowing the
// generic type parameter
public interface IAutomaticEntityTypeConfiguration
{
  void AddConfiguration(ConfigurationRegistrar registrar);
}

// A derived EntityTypeConfiguration that can add itself to the ConfigurationRegistrar
public class AutomaticEntityTypeConfiguration&lt;TEntity&gt;
  : EntityTypeConfiguration&lt;TEntity&gt;, IAutomaticEntityTypeConfiguration
  where TEntity : class
{
  public AutomaticEntityTypeConfiguration()
  {
  }

  public void AddConfiguration(ConfigurationRegistrar registrar)
  {
    registrar.Add(this);
  }
}</code></pre>

<p>Finally, the last class we'll need ties the user code (<code>IEntityTypeConfiguration&lt;TEntity&gt;</code>) and the <code>EntityTypeConfiguration</code> implementation (<code>AutomaticEntityTypeConfiguration&lt;TEntity&gt;</code>) together.</p>

<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">public interface IEntityTypeConfigurationAdapter
{
  void Configure(IEntityTypeConfiguration configurationInterface,
    IAutomaticEntityTypeConfiguration configuration);
}

public class EntityTypeConfigurationAdapter&lt;TEntity&gt;
  : IEntityTypeConfigurationAdapter where TEntity : class
{
  public void Configure(IEntityTypeConfiguration configurationInterface,
    IAutomaticEntityTypeConfiguration configuration)
  {
    IEntityTypeConfiguration&lt;TEntity&gt; typedConfigurationInterface
      = (IEntityTypeConfiguration&lt;TEntity&gt;)configurationInterface;
    typedConfigurationInterface.Configure((EntityTypeConfiguration&lt;TEntity&gt;)configuration);
  }
}</code></pre>

<p>Now that we have all of the needed classes defined, I can show you the code I have in my <code>DbContext</code> to find, configure, and register entity type configurations.</p>

<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">protected override void OnModelCreating(DbModelBuilder modelBuilder)
{

  // Get all entity type configurations
    Dictionary&lt;Type, List&lt;IEntityTypeConfiguration&gt;&gt; typeConfigurations
    = GetTypeConfigurations();

  // Add all the type configurations
  foreach(KeyValuePair&lt;Type, List&lt;IEntityTypeConfiguration&gt;&gt; kvp in typeConfigurations)
  {
    // Create an automatic entity type configurator
    Type configType = typeof(AutomaticEntityTypeConfiguration&lt;&gt;).MakeGenericType(kvp.Key);
    IAutomaticEntityTypeConfiguration configuration
      = (IAutomaticEntityTypeConfiguration)Activator.CreateInstance(configType);

    // Apply the configurations
    ApplyConfigurations(kvp.Key, kvp.Value, configuration);                

    // Add the configuration to the registry
    configuration.AddConfiguration(modelBuilder.Configurations);
  }

}</code></pre>

<p>The <code>GetTypeConfigurations</code> method uses reflection to scan the same assembly as your <code>DbContext</code>, find all <code>IEntityTypeConfiguration</code> implementers, instantiate them, and record them in a list-per-entity-type. Note that this code uses the ");

            
            #line 122 "..\..\Views\Blog\Posts\custom-entity-type-configurations-in-entity-framework-code-first-part-2.cshtml"
                                                                                                                                                                                                                                                                             Write(Html.Post("AddMulti extension method", x => x.quick_and_dirty_multiple_value_dictionary_using_extension_methods));

            
            #line default
            #line hidden
WriteLiteral(" for working with multi-value dictionaries. You could just as easily expand that " +
"part of the code to check if there is already a <code>List&lt;&gt;</code> availa" +
"ble and create one if not.</p>\r\n\r\n<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">private Dictionary&lt;Type, List&lt;IEntityTypeConfiguration&gt;&gt; GetTypeConfigurations()
{  
  Dictionary&lt;Type, List&lt;IEntityTypeConfiguration&gt;&gt; typeConfigurations
    = new Dictionary&lt;Type, List&lt;IEntityTypeConfiguration&gt;&gt;();
  var typesToRegister = Assembly.GetAssembly(typeof(YourDbContext)).GetTypes()
    .Where(type =&gt; type.Namespace != null
      &amp;&amp; type.Namespace.Equals(typeof(YourDbContext).Namespace))
    .Where(type =&gt; type.BaseType.IsGenericType
      &amp;&amp; type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration&lt;&gt;));
  foreach (var configurationType in typesToRegister)
  {
    IEntityTypeConfiguration configurationInstance
      = (IEntityTypeConfiguration)Activator.CreateInstance(configurationType);
    foreach (Type entityType in configurationType
      .GetGenericInterfaces(typeof(IEntityTypeConfiguration))
      .Select(i =&gt; i.GetGenericArguments()[0]))
      {
        typeConfigurations.AddMulti(entityType, configurationInstance);
      }
   {
}</code></pre>

<p>You'll also need the following extension method somewhere:</p>

<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">// This returns all generic interfaces implemented by a given type that themselves implement a given base interface
public static IEnumerable&lt;Type&gt; GetGenericInterfaces(this Type type, Type baseInterfaceType)
{
    Validate.That(
        type.IsNot().Null(() =&gt; type),
        baseInterfaceType.IsNot().Null(() =&gt; baseInterfaceType));
    return type.GetInterfaces().Where(i =&gt; i.IsGenericType &amp;&amp; baseInterfaceType.IsAssignableFrom(i));
}</code></pre>

<p>And finally, the <code>ApplyConfigurations</code> method just uses the <code>EntityTypeConfigurationAdapter</code> to tie the <code>IEntityTypeConfiguration</code> and <code>IAutomaticEntityTypeConfiguration</code> classes together.</p>

<pre><code");

WriteLiteral(" class=\"language-csharp\"");

WriteLiteral(@">private void ApplyConfigurations(
  Type entityType,
  IList&lt;IEntityTypeConfiguration&gt; typeConfigurationInterfaces,
  IAutomaticEntityTypeConfiguration configuration)
{
  // Construct an adapter that will help call the typed configuration methods
  Type adapterType = typeof(EntityTypeConfigurationAdapter&lt;&gt;).MakeGenericType(entityType);
  IEntityTypeConfigurationAdapter adapter
    = (IEntityTypeConfigurationAdapter)Activator.CreateInstance(adapterType);

  // Iterate type configuration interfaces and add to the actual configuration
  foreach (IEntityTypeConfiguration typeConfigurationInterface
    in typeConfigurationInterfaces)
  {
    adapter.Configure(typeConfigurationInterface, configuration);
  }
}</code></pre>

<p>Phew. That was a lot of code. Hopefully it made some sense. I realize it was pretty deep and might not have gelled just by reading it in blog format. The best advice I can give is that if this seems like a capability you want or need that you copy to code into some source files, get it to compile, and step through it. That should give you a better understanding of how it all fits together.</p>");

        }
    }
}
#pragma warning restore 1591