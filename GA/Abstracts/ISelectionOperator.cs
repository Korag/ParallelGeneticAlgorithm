using GA.BasicTypes;

namespace GA.Abstracts
{
    internal interface ISelectionOperator
    {
        Individual[] GenerateParentPopulation(Individual[] currentPopulation);
    }
}