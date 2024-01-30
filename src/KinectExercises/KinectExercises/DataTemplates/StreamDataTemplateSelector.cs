using Streams;
using System.Windows;
using System.Windows.Controls;

namespace KinectExercises.DataTemplates
{
    class StreamDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? SimpleStreamTemplate { get; set; }
        public DataTemplate? BodyStreamTemplate { get; set; }
        public DataTemplate? BodyAndColorStreamTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
         => item switch
            {
                ColorImageStream _ => SimpleStreamTemplate,
                DepthImageStream _ => SimpleStreamTemplate,
                InfraredImageStream _ => SimpleStreamTemplate,
                BodyImageStream _ => BodyStreamTemplate,
                _ => null
            };
    }
}
