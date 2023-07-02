using Newtonsoft.Json;
using QuestionnaireAPI.Domain.DataClasses;
using QuestionnaireAPI.Domain.DTO;
using QuestionnaireAPI.Persistence.Interfaces;

namespace WorkQuestionnaire.Domain.Repository
{
    public class QuestionnaireFormRepository : IQuestionnaireFormRepository
    {
        private readonly string _fileName = "questionnaire.json";

        public async Task<Questionnaire> LoadQuestionnaire()
        {
            try
            {
                var result = new Questionnaire();
                using (StreamReader r = new(_fileName))
                {
                    string json = await r.ReadToEndAsync();
                    result = JsonConvert.DeserializeObject<Questionnaire>(json);
                }

                return result ?? new Questionnaire();
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"The required file {_fileName} is missing: {ex}", _fileName);
            }
            catch (JsonReaderException ex)
            {
                throw new JsonReaderException($"An issue has occurred while reading the JSON file for the questionnaire form: {ex}");
            }
            catch (JsonSerializationException ex)
            {
                throw new JsonSerializationException($"An issue has occurred while deserializing the JSON file for the questionnaire form: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An issue has occurred while loading the questionnaire: {ex}");
            }
        }

        public async Task<List<Question>> LoadPaginatedPage(PagingParameters pagingParameters)
        {
            try
            {
                var questionnaire = new Questionnaire();
                var result = new List<Question>();
                using (StreamReader r = new(_fileName))
                {
                    string json = await r.ReadToEndAsync();
                    questionnaire = JsonConvert.DeserializeObject<Questionnaire>(json);
                }

                if(questionnaire is not null)
                {
                    var query = questionnaire.QuestionnaireItems.AsQueryable();

                    if (pagingParameters.SubjectId is not null)
                    {
                        query = query.Where(x => x.Id == pagingParameters.SubjectId).AsQueryable();
                    }

                    if (questionnaire.QuestionnaireItems.Count > 0)
                    {
                        result = query.SelectMany(subject => subject.QuestionnaireItems)
                            .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                            .Take(pagingParameters.PageSize)
                            .ToList();
                    }
                }

                return result ?? new List<Question>();
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"The required file {_fileName} is missing: {ex}", _fileName);
            }
            catch (JsonReaderException ex)
            {
                throw new JsonReaderException($"An issue has occurred while reading the JSON file for the questionnaire page: {ex}");
            }
            catch (JsonSerializationException ex)
            {
                throw new JsonSerializationException($"An issue has occurred while deserializing the JSON file for the questionnaire page: {ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An issue has occurred while loading the questionnaire page: {ex}");
            }
        }
    }
}
